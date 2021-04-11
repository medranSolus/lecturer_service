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
  groups: Group[];

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

  constructor(private scheduleService: ScheduleService) {}

  ngOnInit(): void {
    this.loadGroupsForLoggedUser();
  }

  ngOnDestroy(): void {
    this.groups$.unsubscribe();
  }
  
  setView(view: string) {
    this.view = view;
  }

  private loadGroupsForLoggedUser() {
    if(isEmpty(this.groups)) {
      this.groups$ = this.scheduleService.getGroupsAssignedToLoggedUser()
      .subscribe(groups => {
        this.mapGroupsToEvents(groups);
        this.mapGroupsToScheduleEvents(groups);
      })
    }
    else {
      this.mapGroupsToEvents(this.groups);
      this.mapGroupsToScheduleEvents(this.groups);
    }
  }

  private mapGroupsToEvents(groups: Group[]) {
    const semesterStart = '03.01.2021'
    const semesterEnd = '06.22.2021'
    groups.forEach(group => {
      const startDate = new Date(semesterStart);
      const endDate = new Date(semesterEnd);
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
        const eventEndDate = new Date(startDate);;
        eventStartDate.setHours(group.startHour, group.startMinute);
        eventEndDate.setHours(group.endHour, group.endMinute);
        this.events.push({
          start: eventStartDate,
          end: eventEndDate,
          title: `${group.id}<br>${eventStartDate.getHours()}:${eventStartDate.getMinutes() < 10 ? '0' + eventStartDate.getMinutes() : eventStartDate.getMinutes()} - ${eventEndDate.getHours()}:${eventEndDate.getMinutes()} `
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
        title: `${group.id}<br>${eventStartDate.getHours()}:${eventStartDate.getMinutes() < 10 ? '0' + eventStartDate.getMinutes() : eventStartDate.getMinutes()} - ${eventEndDate.getHours()}:${eventEndDate.getMinutes()} `
      });
    })
  }

}
