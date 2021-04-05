import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router,
    private spinner: NgxSpinnerService) { 
  }

  ngOnInit(): void {
  }

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(32)]),
    rememberMe: new FormControl(false)
  })
  
  hide = true;
  loginError = false;
  errorMessage = '';

  getEmailErrorMessage() {
    if (this.form.controls['email'].hasError('required')) {
      return 'You must enter a value';
    }

    return this.form.controls['email'].hasError('email') ? 'Not a valid email' : '';
  }

  getPasswordErrorMessage() {
    if (this.form.controls['password'].hasError('required')) {
      return 'You must enter a value';
    }
    if (this.form.controls['password'].hasError('minlength')) {
      return 'Password is too short';
    }
    if (this.form.controls['password'].hasError('maxlength')) {
      return 'Password is too long';
    }
    return '';
  }

  getLoginErrorMessage(error): void {
    if(error.errors[0].param == 'email') {
      this.errorMessage = 'Incorrect e-mail';
    }
    else if(error.errors[0].param == 'password') {
      this.errorMessage = 'Incorrect password';
    }
    else {
      this.errorMessage = 'Something went wrong, please try again later';
    }
  }

  onSubmit() {
    if(this.form.valid) {
      this.spinner.show();
      this.authService.login(this.form)
      .subscribe(
        response => {
          this.loginError = false;
          this.spinner.hide();
          this.router.navigate(['tournaments']);
        },
        error => {
          this.loginError = true;
          this.getLoginErrorMessage(error.error);
          this.spinner.hide();
        });
    }
  }
}
