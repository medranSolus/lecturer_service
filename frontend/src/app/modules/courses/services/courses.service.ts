import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Group } from '../../schedule/models/group.model';
import { Course, CourseShort, GroupRequest } from '../models/courses.model';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  readonly baseURL = environment.baseURL + 'course/';

  getAllCourses(): Observable<CourseShort[]> {
    return this.http.get<CourseShort[]>(this.baseURL);
  }

  addNewCourse(body: any): Observable<any> {
    return this.http.post<any>(this.baseURL, body);
  }

  addMultipleCourses(body: any): Observable<any> {
    return this.http.post<any>(this.baseURL + 'batch', body);
  }

  getGroupsToAccept(id: string): Observable<GroupRequest[]> {
    return this.http.get<GroupRequest[]>(environment.baseURL + 'lecturer/notify/' + id)
  }

  acceptLecturer(body) {
    return this.http.post(environment.baseURL + 'group/accept', body);
  }

  constructor(private http: HttpClient) { }
}
