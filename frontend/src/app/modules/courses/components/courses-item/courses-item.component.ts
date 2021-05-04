import { Component, Input, OnInit } from '@angular/core';
import { toLower } from 'lodash';
import { first } from 'rxjs/operators';
import { LecturerService } from 'src/app/modules/lecturers/services/lecturer.service';
import { CourseListItem, CourseShort, CourseType} from '../../models/courses.model';

@Component({
  selector: 'app-courses-item',
  templateUrl: './courses-item.component.html'
})
export class CoursesItemComponent implements OnInit {

  @Input()
  data: CourseListItem;

  filteredData: CourseShort[];
  
  @Input()
  id: number;

  isOpened = false;

  constructor(private lecturerService: LecturerService) { }

  ngOnInit(): void {
    this.filteredData = this.data.courses;
  }

  getCourseType(course: CourseShort): string {
    return CourseType[course.typeID];
  }

  filterByID(value: string) {
    this.filteredData = this.data.courses.filter(course => toLower(course.id).includes(toLower(value)));
    
  }

  filterByName(value: string) {
    this.filteredData = this.data.courses.filter(course => toLower(course.name).includes(toLower(value)));
  }

  filterByType(value: string) {
    this.filteredData = this.data.courses.filter(course => toLower(this.getCourseType(course)).includes(toLower(value)));
  }
}
