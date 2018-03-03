import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { backOfficeRouting } from './backoffice.routing';
import { BackofficeComponent } from './backoffice.component';
import { materialModule } from './backoffice.material';
import { FormsModule } from '@angular/forms';
import { MarkdownModule } from 'ngx-markdown';
import { SharedModule } from '../shared/shared.module';
import { BackOfficeService } from './services/backoffice.service';
import { EditPostComponent } from './components/editpost/editpost.component';
import { NewPostComponent } from './container/newpost/newpost.component';
import { PostListComponent } from './container/postlist/postlist.component';
import { EditPostComponent as EditPostContainer } from './container/editpost/editpost.component';

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
    PostListComponent,
    EditPostComponent,
    NewPostComponent,
    EditPostContainer
  ],
  providers: [
    BackOfficeService
  ]
})
export class BackOfficeModule { }
