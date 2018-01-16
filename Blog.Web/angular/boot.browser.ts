import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module.browser';

// // Enable either Hot Module Reloading or production mode

enableProdMode();


const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);
