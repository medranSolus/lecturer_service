import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable, Subscription } from 'rxjs';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { HttpErrorResponse } from '@angular/common/http';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit, OnDestroy {

  form = new FormGroup({
    login: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required, Validators.minLength(0), Validators.maxLength(32)]),
    rememberMe: new FormControl(false)
  })
  
  hide = true;
  loginError = false;
  errorMessage = '';
  login$: Subscription;
  user$: Subscription;

  constructor(
    private authService: AuthService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private localStorageService: LocalStorageService) { 
  }


  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.login$.unsubscribe();
    this.user$.unsubscribe();
  }

  getPasswordErrorMessage() {
    if (this.form.controls['password'].hasError('required')) {
      return 'Pole wymagane';
    }
    if (this.form.controls['password'].hasError('minlength')) {
      return 'Hasło jest za krótkie';
    }
    if (this.form.controls['password'].hasError('maxlength')) {
      return 'Hasło jest za długie';
    }
    return '';
  }

  getLoginErrorMessage(error: HttpErrorResponse): void {
    if (error.status === 401) {
      this.errorMessage = 'Podane dane są nieprawidłowe'; 
    }
    else {
      this.errorMessage = 'Coś poszło nie tak, spróbuj ponownie później'; 
    }
  }

  onSubmit() {
    if(this.form.valid) {
      this.spinner.show();
      const login = this.form.get('login').value;
      const password = this.form.get('password').value;
      const rememberMe = this.form.get('rememberMe').value;
      this.login$ = this.authService.login(login, password, rememberMe)
        .subscribe(
          response => {
            this.loginError = false;
            this.loadUser(rememberMe);
          },
          error => {
            this.loginError = true;
            this.getLoginErrorMessage(error);
            this.spinner.hide();
          });
    }
  }

  private loadUser(rememberMe: boolean) {
    this.user$ = this.authService.loadUserData()
      .pipe(first())
      .subscribe(user => {
        this.localStorageService.setUserData(user, rememberMe);
        this.spinner.hide();
        this.router.navigate(['courses']);
      })
  }
}
