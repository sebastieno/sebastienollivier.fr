import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ORIGIN_URL, REQUEST } from '@nguniversal/aspnetcore-engine';
import { AppModuleShared } from './app.module';
import { AppComponent } from './app.component';
import { BrowserTransferStateModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AutService } from './services/aut.service';

export function getOriginUrl() {
  return window.location.origin;
}

export function getRequest() {
  return { cookie: document.cookie };
}

export function handleToken(autService: AutService) {
  return () => autService.handleAuthentication();
}

@NgModule({
  bootstrap: [AppComponent],
  imports: [
    BrowserAnimationsModule,
    AppModuleShared
  ],
  providers: [
    {
      provide: ORIGIN_URL,
      useFactory: getOriginUrl
    },
    { provide: APP_INITIALIZER, useFactory: handleToken, deps: [AutService], multi: true },
    {
      provide: REQUEST,
      useFactory: getRequest
    }
  ]
})
export class AppModule { }
