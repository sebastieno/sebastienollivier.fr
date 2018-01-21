import { NgModule, Inject, PLATFORM_ID } from '@angular/core';
import { RouterModule, PreloadAllModules } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatCardModule, MatIconModule } from '@angular/material';
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
import { AppComponent } from './app.component';
import { HomeComponent } from './containers/home/home.component';
import { NotFoundComponent } from './containers/not-found/not-found.component';
import { BlogService } from './services/blog.service';
import { appConfig } from '../config';
import { BackofficeComponent } from './containers/backoffice/backoffice.component';
import { PostPreviewComponent } from './components/post-preview/post-preview.component';
import { MomentPipe } from './shared/moment.pipe';
import { PostComponent } from './containers/post/post.component';
import { PrebootModule } from 'preboot';
import { ServerTransition } from './server-transition.module';
import { ScrollContainerComponent } from './components/scroll-container/scroll-container.component';
import { DisqusModule } from 'ngx-disqus';
import '../rx-imports';
import { HeaderComponent } from './components/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    BackofficeComponent,
    PostPreviewComponent,
    MomentPipe,
    PostComponent,
    ScrollContainerComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    PrebootModule.withConfig({ appRoot: 'app-root', noReplay: true }),
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    DisqusModule.forRoot('blog-ovent'),
    // ServerTransition.forRoot({ appId: 'my-app-id' }),
    BrowserModule.withServerTransition({ appId: 'my-app-idds' }),
    HttpClientModule,
    TransferHttpCacheModule,
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
            title: 'Homepage',
            state: 'home'
          }
        },
        {
          path: 'back',
          component: BackofficeComponent,
          data: {
            title: 'BackOffice'
          }
        },

        {
          path: 'posts/:categoryCode/:postUrl',
          component: PostComponent,
          data: {
            title: 'Article',
            state: 'post'
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
    BlogService
  ],
  bootstrap: [AppComponent]
})
export class AppModuleShared { }
