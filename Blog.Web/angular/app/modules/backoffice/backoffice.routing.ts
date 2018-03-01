import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BackofficeComponent } from './backoffice.component';
import { NewPostComponent } from './components/newpost/newpost.component';

const routes: Routes = [
  {
    path: '', component: BackofficeComponent, children: [
      { path: '', component: NewPostComponent }
    ]
  }
];

export const backOfficeRouting: ModuleWithProviders = RouterModule.forChild(routes);
