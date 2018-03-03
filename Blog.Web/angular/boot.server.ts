import { enableProdMode } from '@angular/core';
import 'zone.js';
export { AppModule } from './app/app.module.server';
export { BackOfficeModule } from './app/modules/backoffice/backoffice.module';
export { createServerRenderer } from 'aspnet-prerendering';
export { createTransferScript } from '@nguniversal/aspnetcore-engine';
export { provideModuleMap } from '@nguniversal/module-map-ngfactory-loader';
export { ngAspnetCoreEngine } from '@nguniversal/aspnetcore-engine';

enableProdMode();
