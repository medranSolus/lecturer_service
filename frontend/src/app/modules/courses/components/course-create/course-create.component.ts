import { Component, OnChanges, OnInit, SimpleChange, SimpleChanges } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Serialize } from 'cerialize';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { Lecturer } from 'src/app/modules/lecturers/models/lecturer.model';
import { LecturerService } from 'src/app/modules/lecturers/services/lecturer.service';
import { Course, CourseType, Departments, LanguageType } from '../../models/courses.model';
import { Semester } from '../../models/semester.model';
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
    SemesterID: new FormControl('', Validators.required),
    Year: new FormControl(2021, Validators.required),
    LecturerID: new FormControl('', Validators.required)
  });
  
  departments = Departments;
  courseType = CourseType;
  language = LanguageType;
  lecturers: Lecturer[];
  semesters: Semester[];
    
  constructor(
    public dialogRef: MatDialogRef<CourseCreateComponent>, 
    private courseService: CoursesService, 
    private lecturerService: LecturerService,
    private spinner: NgxSpinnerService,) { }

  ngOnInit(): void {
    this.spinner.show()
    this.lecturerService.getAllLecturers()
    .pipe(first())
    .subscribe(lecturers => {
      this.lecturers = lecturers;
    });
    this.courseService.getAllSemesters()
    .pipe(first())
    .subscribe(semesters => {
      this.semesters = semesters;
    })
  }

  exit(): void {
    this.dialogRef.close();
  }

  create() {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      this.spinner.show();
      this.courseService.addNewCourse(JSON.stringify(this.form.getRawValue()))
      .pipe(first())
      .subscribe(response => {
        this.spinner.hide();
        this.dialogRef.close({success: true});
      })
    }
  }

  isInvalid(control: AbstractControl) {
    return control.invalid && control.touched;
  }

  isLoaded(): boolean {
    if (this.lecturers && this.semesters) {   
      this.spinner.hide();
      return true;
    }
    return false;
  }

}
