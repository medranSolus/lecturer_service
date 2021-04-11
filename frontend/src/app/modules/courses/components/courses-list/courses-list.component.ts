import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CourseListItem, CourseShort, Departments } from '../../models/courses.model';
import { CoursesService } from '../../services/courses.service';
import { isEmpty } from 'lodash'

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html'
})
export class CoursesListComponent implements OnInit, OnDestroy {

  courses$: Subscription;
  courseList: CourseListItem[] = [];

  constructor(private coursesService: CoursesService) { }

  ngOnInit(): void {
    this.getAllCourses();
  }

  ngOnDestroy(): void {
    this.courses$.unsubscribe();
  }

  private getAllCourses() {
    this.courses$ = this.coursesService.getAllCourses()
      .subscribe(courses => {
        this.parseCoursesByDepartment(courses);
      });
  }

  private parseCoursesByDepartment(courses: CourseShort[]) {
    for(let i = 0; i < Departments.length; i++) {
      const coursesByDepartment = courses.filter(course => course.departmentID === i);
      if(!isEmpty(coursesByDepartment)) {
        this.courseList.push({departmentID: i, departmentName: Departments[i], courses: coursesByDepartment});
      }
    }
  }

}
