import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/guards/auth.guard';
import { CoursesListComponent } from './components/courses-list/courses-list.component';
import { GroupsToAcceptComponent } from './components/groups-to-accept/groups-to-accept.component';


const routes: Routes = [
  {
    path: 'courses', component: CoursesListComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'groups', component: GroupsToAcceptComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CoursesRoutingModule { }
