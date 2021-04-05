import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-courses-item',
  templateUrl: './courses-item.component.html'
})
export class CoursesItemComponent implements OnInit {

  @Input()
  faculty: string;

  @Input()
  courses: any;

  @Input()
  id: number;

  isOpened = false;

  constructor() { }

  ngOnInit(): void {
  }

}
