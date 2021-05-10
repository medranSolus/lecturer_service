import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { Group } from '../../models/group.model';
import { ScheduleService } from '../../services/schedule.service';
import { CreateGroupComponent } from '../create-group/create-group.component';

@Component({
  selector: 'app-schedule-group-picker',
  templateUrl: './schedule-group-picker.component.html'
})
export class ScheduleGroupPickerComponent implements OnInit {

  groups: Group[];
  id: string;

  constructor (
    private route: ActivatedRoute,
    private scheduleService: ScheduleService,
    public dialog: MatDialog,
    private localStorageService: LocalStorageService) {
  
  }


  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('courseID');
    this.scheduleService.getGroupsAssignedtoCourse(this.id)
      .pipe(first())
      .subscribe(groups => {
        this.groups = groups.filter(group => !group.lecturerID);
      })
  }

  createNewGroup() {
    const dialogRef = this.dialog.open(CreateGroupComponent, { disableClose: true, data: {courseID: this.id} });
    dialogRef.afterClosed()
    .pipe(first())
    .subscribe(result => {
      if (result) {
        this.groups = []
        this.scheduleService.getGroupsAssignedtoCourse(this.id)
        .pipe(first())
        .subscribe(groups => {
          this.groups = groups.filter(group => !group.lecturerID);
        })
      }
    })
  }

  isAdmin() {
    return this.localStorageService.getUserData().roleTypeID === 0;
  }
}
