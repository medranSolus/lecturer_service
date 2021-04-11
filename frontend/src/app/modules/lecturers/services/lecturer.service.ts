import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Lecturer } from '../models/lecturer.model';

@Injectable({
  providedIn: 'root'
})
export class LecturerService {

  readonly baseURL = environment.baseURL + 'lecturer/';

  getAllLecturers(): Observable<Lecturer[]> {
    return this.http.get<Lecturer[]>(this.baseURL);
  }

  getLecturerByID(id: string): Observable<Lecturer> {
    return this.http.get<Lecturer>(this.baseURL + id);
  }

  constructor(private http: HttpClient) { }
}