import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { Group } from '../../models/group.model';
import { ScheduleService } from '../../services/schedule.service';

@Component({
  selector: 'app-schedule-group-picker',
  templateUrl: './schedule-group-picker.component.html'
})
export class ScheduleGroupPickerComponent implements OnInit {

  groups: Group[];

  constructor (
    private route: ActivatedRoute,
    private scheduleService: ScheduleService) {
  
  }


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('courseID');
    this.scheduleService.getGroupsAssignedtoCourse(id)
      .pipe(first())
      .subscribe(groups => {
        this.groups = groups.filter(group => !group.lecturerID);
      })
  }

}
