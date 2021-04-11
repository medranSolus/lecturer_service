import { Component, Input, OnInit } from '@angular/core';
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

  @Input()
  id: number;

  isOpened = false;

  constructor(private lecturerService: LecturerService) { }

  ngOnInit(): void {
  }

  getCourseType(course: CourseShort): string {
    return CourseType[course.typeID];
  }
}
