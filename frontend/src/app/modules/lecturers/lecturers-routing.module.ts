import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../../guards/auth.guard';
import { LecturerListComponent } from './components/lecturer-list/lecturer-list.component';


const routes: Routes = [
  {
    path: 'lecturers', component: LecturerListComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LecturersRoutingModule { }
