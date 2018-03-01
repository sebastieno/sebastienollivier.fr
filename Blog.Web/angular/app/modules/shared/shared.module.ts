import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MomentPipe } from './pipe/moment.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    MomentPipe,
  ],
  exports: [
    MomentPipe
  ]
})
export class SharedModule { }
