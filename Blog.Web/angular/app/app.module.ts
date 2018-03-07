import { HeaderComponent } from '@bw/components';
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
import { AutInterceptor } from './interceptor/aut.interceptor';
import { TransferHttpCacheModule } from '@nguniversal/common';
import { DisqusModule } from 'ngx-disqus';
import '../rx-imports';
import { SharedModule } from './modules/shared/shared.module';
import { materialModule } from './app.module.material';
import { RelativeUrlInterceptor } from './interceptor/relativeurl.interceptor';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    PostComponent,
    HeaderComponent,
  ],
  imports: [
    SharedModule,
    ServiceWorkerModule.register('/ngsw-worker.js', {enabled: environment.production}),
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
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RelativeUrlInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModuleShared { }
