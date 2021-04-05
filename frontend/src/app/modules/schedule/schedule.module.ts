import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ScheduleRoutingModule } from './schedule-routing.module';
import { ScheduleBaseComponent } from './components/schedule-base/schedule-base.component';


@NgModule({
  declarations: [ScheduleBaseComponent],
  imports: [
    CommonModule,
    ScheduleRoutingModule,
  ]
})
export class ScheduleModule { }
