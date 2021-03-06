import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { LocalStorageService } from './local-storage.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { Lecturer } from '../modules/lecturers/models/lecturer.model';

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

  public login(login: string, password: string, rememberMe: boolean): Observable<any> {
    const body = {
      "Login": login,
      "Password": password
    }

    return this.http.post(this.baseURL + 'login', body)
      .pipe(tap(
        res => {
          this.storageService.setUserToken(res.token, rememberMe);
          const tokenPayload = this.jwtHelper.decodeToken(res.token);
          this.storageService.setUserID(tokenPayload.LecturerIdentifier, rememberMe);
        }
      ));
  }

  public loadUserData() {
    const userID = this.storageService.getUserID();
    return this.http.get<Lecturer>(this.baseURL + 'lecturer/' + userID);
  }

  public logout() {
    this.storageService.logout();
    this.router.navigate(['/login']);
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
