import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { LecturerService } from 'src/app/modules/lecturers/services/lecturer.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { CourseType, GroupRequest, GroupsByID } from '../../models/courses.model';
import { CoursesService } from '../../services/courses.service';
import { AcceptLecturerComponent } from '../accept-lecturer/accept-lecturer.component';



@Component({
  selector: 'app-groups-to-accept',
  templateUrl: './groups-to-accept.component.html'
})
export class GroupsToAcceptComponent implements OnInit, OnDestroy {

  groups$: Subscription;
  groups: GroupRequest[];
  groupsById: GroupsByID[] = [];
  courseType = CourseType;

  constructor(private coursesService: CoursesService,
    private localStorageService: LocalStorageService,
    private lecturerService: LecturerService,
    public dialog: MatDialog) { }

  ngOnDestroy(): void {
    if (this.groups$) {
      this.groups$.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.loadData()
  }

  loadData() {
    if (this.groups$) {
      this.groups$.unsubscribe();
    }
    this.groupsById = []
    const id = this.localStorageService.getUserID();
    this.groups$ = this.coursesService.getGroupsToAccept(id)
      .subscribe(groups => {
        groups.forEach(group => {
          this.lecturerService.getLecturerByID(group.group.lecturerID)
            .pipe(first())
            .subscribe(lecturer => {
              group.group.lecturerName = lecturer.name;
              group.group.lecturerSurname = lecturer.surname;
              const groupById = this.groupsById.findIndex(item => item.id === group.group.id);
              if (groupById !== -1) {
                this.groupsById[groupById].groups.push(group);
              } else {
                this.groupsById.push(new GroupsByID(group.group.id, group));
              }
            })
        })
      })
  }

  acceptLecturer(data: GroupRequest) {
    const dialogRef = this.dialog.open(AcceptLecturerComponent, { disableClose: true, data: {data: data} });
    dialogRef.afterClosed()
    .pipe(first())
    .subscribe(result => {
      if(result) {
        this.loadData();
      }
    })
  }

}
