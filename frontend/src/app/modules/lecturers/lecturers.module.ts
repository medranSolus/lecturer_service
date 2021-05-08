import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LecturersRoutingModule } from './lecturers-routing.module';
import { LecturerListComponent } from './components/lecturer-list/lecturer-list.component';
import { LecturerItemComponent } from './components/lecturer-item/lecturer-item.component';
import { AddLecturerComponent } from './components/add-lecturer/add-lecturer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';


@NgModule({
  declarations: [LecturerListComponent, LecturerItemComponent, AddLecturerComponent],
  imports: [
    CommonModule,
    LecturersRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule
  ]
})
export class LecturersModule { }
