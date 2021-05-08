import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  readonly baseURL = environment.baseURL + 'lecturer/';

  updateLecturerData(body) {
    return this.http.put(this.baseURL, body);
  }

  updateLecturerPassword(body) {
    return this.http.put(this.baseURL + 'pass', body);
  }

  constructor(private http: HttpClient) { }
}
