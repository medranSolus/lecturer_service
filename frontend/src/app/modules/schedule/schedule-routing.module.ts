import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../guards/auth.guard';
import { ScheduleGroupPickerComponent } from './components/schedule-group-picker/schedule-group-picker.component';
import { ScheduleMainComponent } from './components/schedule-main/schedule-main.component';


const routes: Routes = [
  {
    path: 'schedule', component: ScheduleMainComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'groups/:courseID', component: ScheduleGroupPickerComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ScheduleRoutingModule { }
