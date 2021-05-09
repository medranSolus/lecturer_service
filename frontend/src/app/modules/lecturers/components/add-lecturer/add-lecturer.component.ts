import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { CourseCreateComponent } from 'src/app/modules/courses/components/course-create/course-create.component';
import { CoursesService } from 'src/app/modules/courses/services/courses.service';
import { Roles } from '../../models/lecturer.model';
import { LecturerService } from '../../services/lecturer.service';

@Component({
  selector: 'app-add-lecturer',
  templateUrl: './add-lecturer.component.html'
})
export class AddLecturerComponent implements OnInit {

  form: FormGroup;
  error = false;
  success = false;
  roles = Roles;
  password = new FormControl('', Validators.required);
  
  constructor(
    public dialogRef: MatDialogRef<CourseCreateComponent>, 
    private courseService: CoursesService, 
    private lecturerService: LecturerService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      Name: new FormControl('', Validators.required),
      Surname:  new FormControl('', Validators.required),
      Mail: new FormControl('', [Validators.email, Validators.required]),
      Phone: new FormControl('', Validators.required),
      RoleTypeID: new FormControl(null, Validators.required),
      ID: new FormControl('', Validators.required),
      Title: new FormControl('', Validators.required)
    });
  }

  create() {
    this.form.markAllAsTouched()
    this.password.markAsTouched();
    if (this.form.valid && this.password.valid) {
      this.spinner.show();
      const lecturer = JSON.stringify(this.form.getRawValue());
      this.lecturerService.addNewLecturer(lecturer, this.password.value)
      .pipe(first())
      .subscribe(
        response => {
          this.error = false;
          this.success = true;
          this.spinner.hide();
          this.exit(true)
        },
        error => {
          this.error = true;
          this.success = false;
          this.spinner.hide();
          this.exit(false)
        }
      )
    }
  }

  exit(success: boolean): void {
    this.dialogRef.close({success: success});
  }

  isInvalid(control: AbstractControl) {
    return control.invalid && control.touched;
  }
}
