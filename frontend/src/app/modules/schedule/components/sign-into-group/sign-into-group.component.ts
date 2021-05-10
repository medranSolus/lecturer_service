import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { ScheduleService } from '../../services/schedule.service';

export interface DialogData {
  groupID: string;
}

@Component({
  selector: 'app-sign-into-group',
  templateUrl: './sign-into-group.component.html'
})
export class SignIntoGroupComponent implements OnInit {

  signError = false

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public dialogRef: MatDialogRef<SignIntoGroupComponent>, 
    private scheduleService: ScheduleService,
    private spinner: NgxSpinnerService,
    private router: Router) { }

  ngOnInit(): void {
  }

  signIn() {
    this.spinner.show()
    this.scheduleService.requestAssignmentToGroup(this.data.groupID)
    .pipe(first())
    .subscribe(
      response => {
        this.spinner.hide();
        this.router.navigateByUrl('schedule')
        this.exit();
      },
      error => {
        this.signError = true;
        this.spinner.hide();
      }
    )
  }

  exit(): void {
    this.dialogRef.close();
  }
}
