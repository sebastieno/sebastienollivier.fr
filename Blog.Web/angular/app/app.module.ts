import { NgModule, Inject, PLATFORM_ID } from '@angular/core';
import { RouterModule, PreloadAllModules } from '@angular/router';
import { MatButtonModule, MatCheckboxModule, MatCardModule, MatIconModule } from '@angular/material';
import {
  CommonModule,
  APP_BASE_HREF,
  isPlatformBrowser
} from '@angular/common';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { PostPreviewComponent } from './components/post-preview/post-preview.component';
import { MomentPipe } from './shared/moment.pipe';
import { PostComponent } from './containers/post/post.component';
import { PrebootModule } from 'preboot';
import { ServerTransition } from './server-transition.module';
import { ScrollContainerComponent } from './components/scroll-container/scroll-container.component';
import { DisqusModule } from 'ngx-disqus';
import '../rx-imports';
import { HeaderComponent } from './components/header/header.component';
import { AutInterceptor } from './interceptor/aut.interceptor';
import { AutService } from './services/aut.service';
import { StorageService } from './services/storage.service';
import { BackOfficeModule } from './modules/backoffice/backoffice.module';
import { appRouting } from './app.routing';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    PostPreviewComponent,
    MomentPipe,
    PostComponent,
    ScrollContainerComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    PrebootModule.withConfig({ appRoot: 'app-root', replay: false }),
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    DisqusModule.forRoot('blog-ovent'),
    BrowserModule.withServerTransition({ appId: 'my-app-idds' }),
    HttpClientModule,
    TransferHttpCacheModule,
    FormsModule,
    appRouting
  ],
  providers: [
    BlogService,
    StorageService,
    AutService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AutInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModuleShared { }
