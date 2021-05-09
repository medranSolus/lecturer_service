import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Deserialize } from 'cerialize';
import { toLower } from 'lodash';
import { Observable, Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { Lecturer } from '../../models/lecturer.model';
import { LecturerService } from '../../services/lecturer.service';
import { AddLecturerComponent } from '../add-lecturer/add-lecturer.component';

@Component({
  selector: 'app-lecturer-list',
  templateUrl: './lecturer-list.component.html'
})
export class LecturerListComponent implements OnInit, OnDestroy {

  lecturers$: Subscription;
  lecturers: Lecturer[];
  filteredData: Lecturer[];

  constructor(private lecturerService: LecturerService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadLecturers();
  }
  
  ngOnDestroy(): void {
    this.lecturers$.unsubscribe();
  }

  public createNewUser() {
    const dialogRef = this.dialog.open(AddLecturerComponent, { disableClose: true });
    dialogRef.afterClosed()
    .pipe(first())
    .subscribe(result => {
      if(result) {
        this.loadLecturers();
      }
    });
  }

  filterByTitle(value: string) {
    this.filteredData = this.lecturers.filter(lecturer => toLower(lecturer.title).includes(toLower(value)));
  }

  filterByName(value: string) {
    this.filteredData = this.lecturers.filter(lecturer => toLower(lecturer.name).includes(toLower(value)));
  }

  filterBySurname(value: string) {
    this.filteredData = this.lecturers.filter(lecturer => toLower(lecturer.surname).includes(toLower(value)));
  }

  filterByMail(value: string) {
    this.filteredData = this.lecturers.filter(lecturer => toLower(lecturer.mail).includes(toLower(value)));
  }

  filterByPhone(value: string) {
    this.filteredData = this.lecturers.filter(lecturer => toLower(lecturer.phone).includes(toLower(value)));
  }

  private loadLecturers() {
    if (this.lecturers$) {
      this.lecturers$.unsubscribe();
    }
    this.lecturers$ = this.lecturerService.getAllLecturers()
      .subscribe(lecturers => {
        this.lecturers = Deserialize(lecturers, Lecturer);
        this.filteredData = this.lecturers;
      });
  }

}
