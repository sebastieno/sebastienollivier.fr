import { PostPreviewComponent, HeaderComponent, ScrollContainerComponent } from '@bw/components';
import { HomeComponent, NotFoundComponent, PostComponent } from '@bw/containers';
import { BlogService, StorageService, AutService } from '@bw/services';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CommonModule } from '@angular/common';
import { PrebootModule } from 'preboot';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { appRouting } from './app.routing';
import { materialModule } from './app.module.material';
import { AutInterceptor } from './interceptor/aut.interceptor';
import { TransferHttpCacheModule } from '@nguniversal/common';
import { DisqusModule } from 'ngx-disqus';
import '../rx-imports';
import { SharedModule } from './modules/shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    PostPreviewComponent,
    PostComponent,
    ScrollContainerComponent,
    HeaderComponent
  ],
  imports: [
    SharedModule,
    CommonModule,
    PrebootModule.withConfig({ appRoot: 'app-root', replay: false }),
    DisqusModule.forRoot('blog-ovent'),
    BrowserModule.withServerTransition({ appId: 'my-app-idds' }),
    HttpClientModule,
    TransferHttpCacheModule,
    FormsModule,
    appRouting,
    ...materialModule
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
