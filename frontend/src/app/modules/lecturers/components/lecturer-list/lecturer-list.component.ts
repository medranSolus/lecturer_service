import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Lecturer } from '../../models/lecturer.model';
import { LecturerService } from '../../services/lecturer.service';

@Component({
  selector: 'app-lecturer-list',
  templateUrl: './lecturer-list.component.html'
})
export class LecturerListComponent implements OnInit, OnDestroy {

  lecturers$: Observable<Lecturer[]>;

  constructor(private lecturerService: LecturerService) { }

  ngOnInit(): void {
    this.lecturers$ = this.lecturerService.getAllLecturers();
  }
  
  ngOnDestroy(): void {

  }
}
