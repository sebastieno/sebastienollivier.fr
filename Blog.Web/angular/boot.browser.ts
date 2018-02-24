import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module.browser';
import { environment } from './environments/environment';
// // Enable either Hot Module Reloading or production mode
if (environment.production) {
  enableProdMode();
}


const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);
