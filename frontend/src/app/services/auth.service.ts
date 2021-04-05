import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { LocalStorageService } from './local-storage.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseURL = environment.baseURL;
  private jwtHelper = new JwtHelperService();

  constructor(private http: HttpClient,
              private storageService: LocalStorageService,
              private router: Router) { 

  }

  public login(form: FormGroup): Observable<any> {
    const body = {
      "email": form.get('email').value,
      "password": form.get('password').value
    }

    return this.http.post(this.baseURL + 'auth', body)
      .pipe(tap(
        res => {
          this.storageService.setUserToken(res.data.accessToken, form.get('rememberMe').value);
          const tokenPayload = this.jwtHelper.decodeToken(res.data.accessToken);
          this.storageService.setUserID(tokenPayload._id, form.get('rememberMe').value)
        }
      ));
  }

  public register(form: FormGroup, URL: string) {
    const body = {
      "email": form.get('email').value,
      "password": form.get('password').value,
      "name": form.get('name').value,
      "surname": form.get('surname').value,
      "nick": form.get('nick').value,
    }

    return this.http.post(URL + 'users', body);
  }

  public logout() {
    this.storageService.logout();
    this.router.navigate(['/home']);
  }

  public isUserLogedIn() {
    const userToken = this.storageService.getUserToken();
    if(!userToken || this.jwtHelper.isTokenExpired(userToken)) {
      return false;
    } 
    else {
      return true;
    }
  }
}
