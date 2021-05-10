import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { CourseListItem, CourseShort, Departments } from '../../models/courses.model';
import { CoursesService } from '../../services/courses.service';
import { isEmpty } from 'lodash'
import { MatDialog } from '@angular/material/dialog';
import { CourseCreateComponent } from '../course-create/course-create.component';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html'
})
export class CoursesListComponent implements OnInit, OnDestroy {

  courses$: Subscription;
  courseList: CourseListItem[] = [];

  constructor(private coursesService: CoursesService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getAllCourses();
  }

  ngOnDestroy(): void {
    this.courses$.unsubscribe();
  }

  public createNewCourse() {
    const dialogRef = this.dialog.open(CourseCreateComponent, { disableClose: true });
    dialogRef.afterClosed()
    .pipe(first())
    .subscribe(result => {
      if(result) {
        if(this.courses$) {
          this.courses$.unsubscribe();
        }
        this.courseList = [];
        this.getAllCourses();
      }
    })
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
