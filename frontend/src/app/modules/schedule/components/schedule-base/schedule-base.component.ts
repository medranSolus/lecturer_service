import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import * as moment from 'moment';
import { Subject, Subscription } from 'rxjs';
import {
  CalendarDateFormatter,
  CalendarEvent,
  CalendarView,
  DAYS_OF_WEEK,
} from 'angular-calendar';
import { CustomDateFormatter } from '../../providers/custom-date-formatter.provider';
import { ScheduleService } from '../../services/schedule.service';
import { Group } from '../../models/group.model';
import { isEmpty } from 'lodash';
import { Colors, WeekType } from '../../models/constants.model';
import { CourseType } from 'src/app/modules/courses/models/courses.model';
import { MatDialog } from '@angular/material/dialog';
import { SignIntoGroupComponent } from '../sign-into-group/sign-into-group.component';
@Component({
  selector: 'app-schedule-base',
  templateUrl: './schedule-base.component.html',
  providers: [
    {
      provide: CalendarDateFormatter,
      useClass: CustomDateFormatter,
    },
  ]
})
export class ScheduleBaseComponent implements OnDestroy, OnInit{

  @Input()
  courseGroups: Group[];

  @Input()
  excludeWeekends = false;

  @Input()
  onlySchedule = false;

  view: string = 'schedule';
  CalendarView = CalendarView;
  viewDate: Date = new Date();
  scheduleViewDate: Date = new Date();
  locale: string = 'pl';
  weekStartsOn: number = DAYS_OF_WEEK.MONDAY;
  weekendDays: number[] = [DAYS_OF_WEEK.SATURDAY, DAYS_OF_WEEK.SUNDAY];
  refresh: Subject<any> = new Subject();
  groups$: Subscription;
  events: CalendarEvent[] = [];
  eventsSchedule: CalendarEvent[] = [];
  activeDayIsOpen: boolean = true;
  emptyData = false;

  courseTypes = CourseType;
  excludeDays: number[];

  constructor(private scheduleService: ScheduleService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadGroupsForLoggedUser();
    if (this.excludeWeekends) {
      this.excludeDays = [0, 6];
    }
  }

  ngOnDestroy(): void {
    if (this.groups$) {
      this.groups$.unsubscribe();
    }
  }
  
  setView(view: string) {
    this.view = view;
  }

  private loadGroupsForLoggedUser() {
      this.groups$ = this.scheduleService.getGroupsAssignedToLoggedUser()
      .subscribe(groups => {
        if (isEmpty(groups)) {
          this.emptyData = true
        }
        if (isEmpty(this.courseGroups)) {
          this.mapGroupsToEvents(groups);
          this.mapGroupsToScheduleEvents(groups);
        } else {
          this.mergeLecturerGroupsWithCourseGroups(groups);
        }

      })
  }

  private mapGroupsToEvents(groups: Group[]) {
    groups.forEach(group => {
      const startDate = new Date();
      startDate.setMonth(group.startMonth - 1, group.startDay);
      const endDate = new Date();
      endDate.setMonth(group.endMonth, group.endDay);
      const week = moment(startDate).week() % 2;
      const dayOfWeek = group.dayID + 1;
      const weekType = group.weekTypeID - 1;
      while(startDate.getDay() !== dayOfWeek) {
        startDate.setDate(startDate.getDate() + 1);
      }
      if (weekType !== -1 && weekType !== week) {
        startDate.setDate(startDate.getDate() + 7);
      }
      while(startDate.getTime() <= endDate.getTime()) {
        const eventStartDate = new Date(startDate);
        const eventEndDate = new Date(startDate);
        eventStartDate.setHours(group.startHour, group.startMinute);
        eventEndDate.setHours(group.endHour, group.endMinute);
        this.events.push({
          start: eventStartDate,
          end: eventEndDate,
          title: `${group.courseName}<br> ${group.weekTypeID ? WeekType[group.weekTypeID - 1] : ''} ${eventStartDate.getHours()}:${eventStartDate.getMinutes() < 10 ? '0' + eventStartDate.getMinutes() : eventStartDate.getMinutes()} - ${eventEndDate.getHours()}:${eventEndDate.getMinutes()}<br>${group.room}  ${group.building}`,
          color: Colors[group.courseTypeID]
        });
        if(weekType !== -1) {
          startDate.setDate(startDate.getDate() + 14);
        } else {
          startDate.setDate(startDate.getDate() + 7);
        }
      }
    })
  }

  private mapGroupsToScheduleEvents(groups: Group[]) {
    groups.forEach(group => {
      const startDate = new Date();
      const dayOfWeek = group.dayID + 1;
      while(startDate.getDay() !== dayOfWeek) {
        if(startDate.getDay() > dayOfWeek || startDate.getDay() === 0) {
          startDate.setDate(startDate.getDate() - 1);
        } else {
          startDate.setDate(startDate.getDate() + 1);
        }
      }
      const eventStartDate = new Date(startDate);
      const eventEndDate = new Date(startDate);;
      eventStartDate.setHours(group.startHour, group.startMinute);
      eventEndDate.setHours(group.endHour, group.endMinute);
      this.eventsSchedule.push({
        start: eventStartDate,
        end: eventEndDate,
        title: `${group.courseName}<br> ${group.weekTypeID ? WeekType[group.weekTypeID - 1] : ''} ${eventStartDate.getHours()}:${eventStartDate.getMinutes() < 10 ? '0' + eventStartDate.getMinutes() : eventStartDate.getMinutes()} - ${eventEndDate.getHours()}:${eventEndDate.getMinutes()}<br>${group.room}  ${group.building}`,
        color: Colors[group.courseTypeID]
      });
    })
  }

  private mergeLecturerGroupsWithCourseGroups(groups: Group[]) {
    groups.forEach(group => {
      const startDate = new Date();
      const dayOfWeek = group.dayID + 1;
      while(startDate.getDay() !== dayOfWeek) {
        if(startDate.getDay() > dayOfWeek || startDate.getDay() === 0) {
          startDate.setDate(startDate.getDate() - 1);
        } else {
          startDate.setDate(startDate.getDate() + 1);
        }
      }
      const eventStartDate = new Date(startDate);
      const eventEndDate = new Date(startDate);;
      eventStartDate.setHours(group.startHour, group.startMinute);
      eventEndDate.setHours(group.endHour, group.endMinute);

      this.eventsSchedule.push({
        start: eventStartDate,
        end: eventEndDate,
        title: `${group.id}<br> ${group.weekTypeID ? WeekType[group.weekTypeID - 1] : ''} ${eventStartDate.getHours()}:${eventStartDate.getMinutes() < 10 ? '0' + eventStartDate.getMinutes() : eventStartDate.getMinutes()} - ${eventEndDate.getHours()}:${eventEndDate.getMinutes()}<br>${group.room}  ${group.building}`,
        color: Colors[2]
      });
    });
    this.courseGroups.forEach(group => {
      const repeatedGroup = groups.find(item => item.id === group.id);
      if (!repeatedGroup) {
        const startDate = new Date();
        const dayOfWeek = group.dayID + 1;
        while(startDate.getDay() !== dayOfWeek) {
          if(startDate.getDay() > dayOfWeek || startDate.getDay() === 0) {
            startDate.setDate(startDate.getDate() - 1);
          } else {
            startDate.setDate(startDate.getDate() + 1);
          }
        }
        const eventStartDate = new Date(startDate);
        const eventEndDate = new Date(startDate);;
        eventStartDate.setHours(group.startHour, group.startMinute);
        eventEndDate.setHours(group.endHour, group.endMinute);

        this.eventsSchedule.push({
          id: group.id,
          start: eventStartDate,
          end: eventEndDate,
          title: `${group.id}<br>${group.weekTypeID ? WeekType[group.weekTypeID - 1] : ''} ${eventStartDate.getHours()}:${eventStartDate.getMinutes() < 10 ? '0' + eventStartDate.getMinutes() : eventStartDate.getMinutes()} - ${eventEndDate.getHours()}:${eventEndDate.getMinutes()}<br>${group.room}  ${group.building}`,
          color: Colors[1]
        });
      }
    });
  }

  signIntoGroup(event) {
    if (event.event.id) {
      const dialogRef = this.dialog.open(SignIntoGroupComponent, { disableClose: true, data: {groupID: event.event.id} });
    }
  }

}
