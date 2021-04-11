import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { FlatpickrModule } from 'angularx-flatpickr';

import { ScheduleRoutingModule } from './schedule-routing.module';
import { ScheduleBaseComponent } from './components/schedule-base/schedule-base.component';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';
import { ScheduleGroupPickerComponent } from './components/schedule-group-picker/schedule-group-picker.component';
import { ScheduleMainComponent } from './components/schedule-main/schedule-main.component';

registerLocaleData(localePl);
@NgModule({
  declarations: [ScheduleBaseComponent, ScheduleGroupPickerComponent, ScheduleMainComponent],
  imports: [
    CommonModule,
    ScheduleRoutingModule,
    FormsModule,
    NgbModalModule,
    FlatpickrModule.forRoot(),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
  ]
})
export class ScheduleModule { }
