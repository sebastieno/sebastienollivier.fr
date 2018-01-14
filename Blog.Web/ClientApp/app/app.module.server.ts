import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { BrowserModule } from '@angular/platform-browser';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { AppModuleShared } from './app.module';
import { AppComponent } from './app.component';
import { ServerTransferStateModule } from '@angular/platform-server';

import { ServerPrebootModule } from 'preboot/server';

@NgModule({
  bootstrap: [AppComponent],
  imports: [
    NoopAnimationsModule,
    AppModuleShared,
    ServerModule,
    ServerPrebootModule.recordEvents({ appRoot: 'app-root' }),
    // ServerTransferStateModule // <-- broken for the time-being with ASP.NET
  ]
})
export class AppModule { }
