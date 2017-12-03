import { CanActivateViaAuthGuard } from './shared/canActivateAuthGuard';
import { NgModule, Inject, PLATFORM_ID } from '@angular/core';
import { RouterModule, PreloadAllModules } from '@angular/router';
import {
  CommonModule,
  APP_BASE_HREF,
  isPlatformBrowser
} from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {
  BrowserModule,
  BrowserTransferStateModule
} from '@angular/platform-browser';
import { TransferHttpCacheModule } from '@nguniversal/common';
import { AuthenticationContext, AdalConfig, Authentication } from 'adal-ts';
import { AppComponent } from './app.component';
import { HomeComponent } from './containers/home/home.component';
import { NotFoundComponent } from './containers/not-found/not-found.component';
import { BlogService } from './services/blog.service';
import { ORIGIN_URL } from '@nguniversal/aspnetcore-engine';
import { appConfig } from '../config';
import { BackofficeComponent } from './containers/backoffice/backoffice.component';
import { PostPreviewComponent } from './components/post-preview/post-preview.component';
import { MomentPipe } from './shared/moment.pipe';
import { PostComponent } from './containers/post/post.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    BackofficeComponent,
    PostPreviewComponent,
    MomentPipe,
    PostComponent
  ],
  imports: [
    CommonModule,
    BrowserModule.withServerTransition({
      appId: 'my-app-id'
    }),
    HttpClientModule,
    TransferHttpCacheModule,
    BrowserTransferStateModule,
    FormsModule,
    RouterModule.forRoot(
      [
        {
          path: '',
          redirectTo: 'home',
          pathMatch: 'full'
        },
        {
          path: 'home',
          component: HomeComponent,
          data: {
            title: 'Homepage'
          }
        },
        {
          path: 'back',
          component: BackofficeComponent,
          canActivate: [CanActivateViaAuthGuard],
          data: {
            title: 'BackOffice'
          }
        },

        {
          path: 'post/:categoryCode/:postUrl',
          component: PostComponent,
          data: {
            title: 'Article'
          }
        },
        {
          path: '**',
          component: NotFoundComponent,
          data: {
            title: '404 - Not found'
          }
        }
      ],
      {
        useHash: false,
        preloadingStrategy: PreloadAllModules,
        initialNavigation: 'enabled'
      }
    )
  ],
  providers: [
    CanActivateViaAuthGuard,
    BlogService,
    {
      provide: AuthenticationContext,
      useFactory: (platformId: Object) => {
        if (isPlatformBrowser(platformId)) {
          let config = new AdalConfig(
            appConfig.clientId,
            appConfig.tenant,
            window.location.origin
          );
          return Authentication.getContext(config);
        } else {
          return null;
        }
      },
      deps: [PLATFORM_ID]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModuleShared { }
