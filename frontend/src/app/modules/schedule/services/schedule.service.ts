import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Group } from '../models/group.model';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  readonly baseURL = environment.baseURL + 'group/';

  getAllGroups(): Observable<Group[]> {
    return this.http.get<Group[]>(this.baseURL);
  }

  getGroupsAssignedToLoggedUser(): Observable<Group[]> {
    return this.http.get<Group[]>(this.baseURL + 'lecturer');
  }

  getGroupsAssignedtoCourse(id: string): Observable<Group[]> {
    return this.http.get<Group[]>(this.baseURL + 'course/' + id)
  }

  requestAssignmentToGroup(id: string) {
    return this.http.post(this.baseURL + id, {})
  }

  constructor(private http: HttpClient) { }
}
