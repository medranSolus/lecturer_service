import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { Deserialize, Serialize } from 'cerialize';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { Lecturer } from 'src/app/modules/lecturers/models/lecturer.model';
import { AuthService } from 'src/app/services/auth.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent implements OnInit {

  form: FormGroup;
  error = false;
  success = false;
  
  constructor(
    private spinner: NgxSpinnerService,
    private localStorageService: LocalStorageService,
    private accountService: AccountService,
    private authService: AuthService) { }

  ngOnInit(): void {
    const user = this.localStorageService.getUserData();
    this.form = new FormGroup({
      Name: new FormControl(user.name, Validators.required),
      Surname:  new FormControl(user.surname, Validators.required),
      Mail: new FormControl(user.mail, [Validators.email, Validators.required]),
      Phone: new FormControl(user.phone, Validators.required),
      RoleTypeID: new FormControl(user.roleTypeID),
      ID: new FormControl(user.id),
      Title: new FormControl(user.title)
    });
  }


  save() {
    this.form.markAllAsTouched()
    if (this.form.valid) {
      this.spinner.show();
      const lecturer = JSON.stringify(this.form.getRawValue());
      this.accountService.updateLecturerData(lecturer)
      .pipe(first())
      .subscribe(
        response => {
          this.error = false;
          this.success = true;
          this.authService.loadUserData()
            .pipe(first())
            .subscribe(user => {
              this.localStorageService.updateUserData(user);
              this.spinner.hide();
            })
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

}
