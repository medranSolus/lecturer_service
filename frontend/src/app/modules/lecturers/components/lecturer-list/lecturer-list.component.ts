import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Lecturer } from '../../models/lecturer.model';
import { LecturerService } from '../../services/lecturer.service';
import { AddLecturerComponent } from '../add-lecturer/add-lecturer.component';

@Component({
  selector: 'app-lecturer-list',
  templateUrl: './lecturer-list.component.html'
})
export class LecturerListComponent implements OnInit, OnDestroy {

  lecturers$: Observable<Lecturer[]>;

  constructor(private lecturerService: LecturerService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.lecturers$ = this.lecturerService.getAllLecturers();
  }
  
  ngOnDestroy(): void {

  }

  public createNewUser() {
    const dialogRef = this.dialog.open(AddLecturerComponent, { disableClose: true });
  }

}
