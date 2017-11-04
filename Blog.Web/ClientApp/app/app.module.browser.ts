import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ORIGIN_URL, REQUEST } from '@nguniversal/aspnetcore-engine';
import { AppModuleShared } from './app.module';
import { AppComponent } from './app.component';
import { BrowserTransferStateModule } from '@angular/platform-browser';
import { BrowserPrebootModule } from 'preboot/browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './shared/token-interceptor';

export function getOriginUrl() {
  return window.location.origin;
}

export function getRequest() {
  return { cookie: document.cookie };
}

@NgModule({
  bootstrap: [AppComponent],
  imports: [
    BrowserPrebootModule.replayEvents(),
    BrowserAnimationsModule,
    AppModuleShared
  ],
  providers: [
    {
      provide: ORIGIN_URL,
      useFactory: getOriginUrl
    },
    {
      provide: REQUEST,
      useFactory: getRequest
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ]
})
export class AppModule {}
