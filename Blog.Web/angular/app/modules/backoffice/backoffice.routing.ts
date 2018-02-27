import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BackofficeComponent } from './components/backoffice/backoffice.component';

const routes: Routes = [
  { path: '', component: BackofficeComponent }
];

export const backOfficeRouting: ModuleWithProviders = RouterModule.forChild(routes);
