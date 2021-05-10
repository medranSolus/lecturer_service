import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { ScheduleService } from '../../services/schedule.service';

export interface DialogData {
  courseID: string;
}
@Component({
  selector: 'app-create-group',
  templateUrl: './create-group.component.html'
})
export class CreateGroupComponent implements OnInit {

  form: FormGroup;
  error = false;
  success = false;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public dialogRef: MatDialogRef<CreateGroupComponent>,
    private spinner: NgxSpinnerService,
    private scheduleService: ScheduleService
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      ID: new FormControl('', Validators.required),
      CourseID:  new FormControl(this.data.courseID),
      StudentsCount: new FormControl(0, [Validators.min(1), Validators.max(1000), Validators.required]),
      Room: new FormControl('', Validators.required),
      Building: new FormControl('', Validators.required),
      WeekTypeID: new FormControl(0, Validators.required),
      StartMonth: new FormControl(3),
      StartDay: new FormControl(1),
      EndMonth: new FormControl(6),
      EndDay: new FormControl(22),
      DayID: new FormControl(0, Validators.required),
      StartHour: new FormControl(9, [Validators.min(7), Validators.max(21), Validators.required]),
      StartMinute: new FormControl(15, [Validators.min(0), Validators.max(60), Validators.required]),
      EndHour: new FormControl(11, [Validators.min(7), Validators.max(22), Validators.required]),
      EndMinute: new FormControl(45, [Validators.min(0), Validators.max(59), Validators.required]),

    });
  }

  create() {
    this.form.markAllAsTouched()
    if (this.form.valid) {
      this.spinner.show();
      const data = JSON.stringify(this.form.getRawValue());
      this.scheduleService.createNewGroup(data)
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
