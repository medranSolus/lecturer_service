import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatDialogModule} from '@angular/material/dialog';

import { CoursesRoutingModule } from './courses-routing.module';
import { CoursesListComponent } from './components/courses-list/courses-list.component';
import { CoursesItemComponent } from './components/courses-item/courses-item.component';
import { CourseCreateComponent } from './components/course-create/course-create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [CoursesListComponent, CoursesItemComponent, CourseCreateComponent],
  imports: [
    CommonModule,
    CoursesRoutingModule,
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
  ]
})
export class CoursesModule { }
