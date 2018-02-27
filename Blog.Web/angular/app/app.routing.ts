import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './containers/home/home.component';
import { PostComponent } from './containers/post/post.component';
import { NotFoundComponent } from './containers/not-found/not-found.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, data: { title: 'Homepage', state: 'home' } },
  { path: 'back', loadChildren: './modules/backoffice/backoffice.module#BackOfficeModule' },
  { path: 'posts/:categoryCode/:postUrl', component: PostComponent, data: { title: 'Article', state: 'post' } },
  { path: '**', component: NotFoundComponent, data: { title: '404 - Not found' } }
];

export const appRouting: ModuleWithProviders = RouterModule.forRoot(routes, { initialNavigation: 'enabled' });
