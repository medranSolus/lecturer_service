import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Serialize } from 'cerialize';
import { first } from 'rxjs/operators';
import { Course, CourseType, Departments } from '../../models/courses.model';
import { CoursesService } from '../../services/courses.service';

@Component({
  selector: 'app-course-create',
  templateUrl: './course-create.component.html'
})
export class CourseCreateComponent implements OnInit {

  form = new FormGroup({
    ID: new FormControl('', Validators.required),
    Accepted:  new FormControl(true),
    Name: new FormControl('', Validators.required),
    DepartmentID: new FormControl(0, Validators.required),
    TypeID: new FormControl(0, Validators.required),
    LanguageTypeID: new FormControl(0, Validators.required),
    Ects: new FormControl(0, Validators.required),
    HoursUniversity: new FormControl(0, Validators.required),
    HoursStudent: new FormControl(0, Validators.required),
    SemesterTypeID: new FormControl(0, Validators.required),
    Year: new FormControl(2021, Validators.required),
  });
  
  departments = Departments;
  courseType = CourseType;

  constructor(public dialogRef: MatDialogRef<CourseCreateComponent>, private courseService: CoursesService) { }

  ngOnInit(): void {
  }

  exit(): void {
    this.dialogRef.close();
  }

  create() {
    this.courseService.addNewCourse(JSON.stringify(this.form.getRawValue()))
      .pipe(first())
      .subscribe(response => {
        console.log(response)
        this.dialogRef.close({});
    })
  }

}
