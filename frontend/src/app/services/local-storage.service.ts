import { Injectable } from '@angular/core';
import { Deserialize, Serialize, serialize } from 'cerialize';
import { Lecturer } from '../modules/lecturers/models/lecturer.model';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  getUserToken(): string {
    return sessionStorage.getItem('userToken') || localStorage.getItem('userToken');
  }

  getUserID(): string {
    return sessionStorage.getItem('userID') || localStorage.getItem('userID');
  }

  setUserToken(token: string, rememberMe: boolean) {
    if(rememberMe) {
      localStorage.setItem('userToken', token);
    }
    else {
      sessionStorage.setItem('userToken', token);
    }
  }

  setUserID(id: string, rememberMe: boolean) {
    if(rememberMe) {
      localStorage.setItem('userID', id);
    }
    else {
      sessionStorage.setItem('userID', id);
    }
  }

  setUserData(user: Lecturer, rememberMe: boolean) {
    const userSerialized = JSON.stringify(user);
    if(rememberMe) {
      localStorage.setItem('userData', userSerialized);
    }
    else {
      sessionStorage.setItem('userData', userSerialized);
    }
  }

  getUserData(): Lecturer {
    const userData = sessionStorage.getItem('userData') || localStorage.getItem('userData');

    return Deserialize(JSON.parse(userData), Lecturer);
  }

  logout() {
    localStorage.removeItem('userToken');
    localStorage.removeItem('userID');
    localStorage.removeItem('userData');
    sessionStorage.removeItem('userToken');
    sessionStorage.removeItem('userID');
    sessionStorage.removeItem('userData');
  }
}
