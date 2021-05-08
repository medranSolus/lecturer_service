import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {

  form: FormGroup;
  error = false;
  success = false;
  
  constructor(
    private spinner: NgxSpinnerService,
    private localStorageService: LocalStorageService,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      password: new FormControl('', Validators.required),
      passwordRepeat: new FormControl('', Validators.required),
    });
  }


  save() {
    this.form.markAllAsTouched()
    if (this.form.valid) {
      this.spinner.show()
      const user = this.localStorageService.getUserData();
      const body = {
        ID: user.id,
        Pass: this.form.controls['password'].value
      }
      this.accountService.updateLecturerData(JSON.stringify(this.form.getRawValue()))
      .pipe(first())
      .subscribe(
        response => {
          this.error = false;
          this.success = true;
          this.spinner.hide();
        },
        error => {
          this.error = true;
          this.success = false;
          this.spinner.hide();
        }
      )
    }
  }

  isInvalid(control: AbstractControl) {
    return control.invalid && control.touched;
  }

  isRepeatInvalid() {
    console.log(this.form.controls['passwordRepeat'].value)
    console.log(this.form.controls['password'].value)
    return (this.form.controls['passwordRepeat'].invalid && this.form.controls['passwordRepeat'].touched) || (this.form.controls['passwordRepeat'].touched && this.form.controls['passwordRepeat'].value != this.form.controls['password'].value);
  }

}
