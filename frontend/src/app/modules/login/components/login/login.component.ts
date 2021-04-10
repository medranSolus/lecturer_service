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
    login: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(32)]),
    rememberMe: new FormControl(false)
  })
  
  hide = true;
  loginError = false;
  errorMessage = '';

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
    this.errorMessage = 'Something went wrong, please try again later';
  }

  onSubmit() {
    if(this.form.valid) {
      this.spinner.show();
      const login = this.form.get('login').value;
      const password = this.form.get('password').value;
      const rememberMe = this.form.get('rememberMe').value;
      this.authService.login(login, password, rememberMe)
        .subscribe(
          response => {
            this.loginError = false;
            this.spinner.hide();
            this.router.navigate(['schedule']);
          },
          error => {
            this.loginError = true;
            this.getLoginErrorMessage(error.error);
            this.spinner.hide();
          });
    }
  }
}
