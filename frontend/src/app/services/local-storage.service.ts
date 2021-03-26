import { Injectable } from '@angular/core';

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

  logout() {
    localStorage.removeItem('userToken');
    localStorage.removeItem('userID');
    sessionStorage.removeItem('userToken');
    sessionStorage.removeItem('userID');
  }
}
