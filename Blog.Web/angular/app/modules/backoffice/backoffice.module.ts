import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { backOfficeRouting } from './backoffice.routing';
import { BackofficeComponent } from './components/backoffice/backoffice.component';

@NgModule({
  imports: [
    CommonModule,
    backOfficeRouting,
  ],
  declarations: [BackofficeComponent]
})
export class BackOfficeModule { }
