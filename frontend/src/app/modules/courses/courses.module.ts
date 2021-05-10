import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatDialogModule} from '@angular/material/dialog';

import { CoursesRoutingModule } from './courses-routing.module';
import { CoursesListComponent } from './components/courses-list/courses-list.component';
import { CoursesItemComponent } from './components/courses-item/courses-item.component';
import { CourseCreateComponent } from './components/course-create/course-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { GroupsToAcceptComponent } from './components/groups-to-accept/groups-to-accept.component';
import { AcceptLecturerComponent } from './components/accept-lecturer/accept-lecturer.component';


@NgModule({
  declarations: [CoursesListComponent, CoursesItemComponent, CourseCreateComponent, GroupsToAcceptComponent, AcceptLecturerComponent],
  imports: [
    CommonModule,
    CoursesRoutingModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
  ]
})
export class CoursesModule { }
