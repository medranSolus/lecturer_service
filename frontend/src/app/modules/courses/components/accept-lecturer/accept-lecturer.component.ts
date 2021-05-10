import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { GroupRequest } from '../../models/courses.model';
import { CoursesService } from '../../services/courses.service';

export interface DialogData {
  data: GroupRequest;
}

@Component({
  selector: 'app-accept-lecturer',
  templateUrl: './accept-lecturer.component.html'
})
export class AcceptLecturerComponent implements OnInit {

  signError = false

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public dialogRef: MatDialogRef<AcceptLecturerComponent>, 
    private coursesService: CoursesService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  ngOnInit(): void {
  }

  signIn() {
    this.spinner.show()
    const body = {
      ID: this.data.data.id,
      GroupID: this.data.data.group.id,
      LecturerID: this.data.data.group.lecturerID
    }
    this.coursesService.acceptLecturer(body)
      .pipe(first())
      .subscribe(
        response => {
          this.signError = false;
          this.spinner.hide();
          this.router.navigateByUrl('groups');
          this.exit(true);
        },
        error => {
          this.signError = true;
          this.spinner.hide();
        }
      )
  }

  exit(success?): void {
    if(success) {
      this.dialogRef.close({succes: success});
    } else {
      this.dialogRef.close();
    }
    
  }
}
