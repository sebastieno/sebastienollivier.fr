import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { backOfficeRouting } from './backoffice.routing';
import { BackofficeComponent } from './backoffice.component';
import { materialModule } from './backoffice.material';
import { NewPostComponent } from './components/newpost/newpost.component';
import { FormsModule } from '@angular/forms';
import { MarkdownModule } from 'ngx-markdown';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    backOfficeRouting,
    SharedModule,
    FormsModule,
    MarkdownModule.forRoot(), // forRoot dans un lazy load car utilisé que ici
    ...materialModule
  ],
  declarations: [
    BackofficeComponent,
    NewPostComponent
  ]
})
export class BackOfficeModule { }
