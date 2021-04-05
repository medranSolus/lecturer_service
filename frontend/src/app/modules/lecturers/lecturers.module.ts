import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LecturersRoutingModule } from './lecturers-routing.module';
import { LecturerListComponent } from './components/lecturer-list/lecturer-list.component';
import { LecturerItemComponent } from './components/lecturer-item/lecturer-item.component';


@NgModule({
  declarations: [LecturerListComponent, LecturerItemComponent],
  imports: [
    CommonModule,
    LecturersRoutingModule
  ]
})
export class LecturersModule { }
