import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BackofficeComponent } from './backoffice.component';
import { NewPostComponent } from './container/newpost/newpost.component';
import { PostListComponent } from './container/postlist/postlist.component';
import { EditPostComponent } from './container/editpost/editpost.component';

const routes: Routes = [
  {
    path: '', component: BackofficeComponent, children: [
      { path: '', component: NewPostComponent },
      { path: 'list', component: PostListComponent },
      { path: 'edit/:categoryCode/:postUrl', component: EditPostComponent },
    ]
  }
];

export const backOfficeRouting: ModuleWithProviders = RouterModule.forChild(routes);
