import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { FlatpickrModule } from 'angularx-flatpickr';

import { ScheduleRoutingModule } from './schedule-routing.module';
import { ScheduleBaseComponent } from './components/schedule-base/schedule-base.component';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { registerLocaleData } from '@angular/common';
import localePl from '@angular/common/locales/pl';
import { ScheduleGroupPickerComponent } from './components/schedule-group-picker/schedule-group-picker.component';
import { ScheduleMainComponent } from './components/schedule-main/schedule-main.component';
import { SignIntoGroupComponent } from './components/sign-into-group/sign-into-group.component';
import { CreateGroupComponent } from './components/create-group/create-group.component';
import { NgxSpinnerModule } from 'ngx-spinner';

registerLocaleData(localePl);
@NgModule({
  declarations: [ScheduleBaseComponent, ScheduleGroupPickerComponent, ScheduleMainComponent, SignIntoGroupComponent, CreateGroupComponent],
  imports: [
    CommonModule,
    ScheduleRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModalModule,
    FlatpickrModule.forRoot(),
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    NgxSpinnerModule
  ],
  exports: [
    ScheduleGroupPickerComponent
  ]
})
export class ScheduleModule { }
