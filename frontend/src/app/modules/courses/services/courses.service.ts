import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CourseShort } from '../models/courses.model';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  readonly baseURL = environment.baseURL + 'course/';

  getAllCourses(): Observable<CourseShort[]> {
    return this.http.get<CourseShort[]>(this.baseURL);
  }

  constructor(private http: HttpClient) { }
}
