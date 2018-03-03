import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MomentPipe } from './pipe/moment.pipe';
import { PostPreviewComponent, ScrollContainerComponent } from '@bw/shared/components';
import { materialModule } from './shared.module.material';

@NgModule({
  imports: [
    CommonModule,
    ...materialModule
  ],
  declarations: [
    MomentPipe,
    PostPreviewComponent,
    ScrollContainerComponent
  ],
  exports: [
    MomentPipe,
    PostPreviewComponent,
    ScrollContainerComponent
  ]
})
export class SharedModule { }
