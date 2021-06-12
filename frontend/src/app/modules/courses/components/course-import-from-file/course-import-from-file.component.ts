import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Deserialize, Serialize } from 'cerialize';
import { NgxCsvParser, NgxCSVParserError } from 'ngx-csv-parser';
import { FileSystemDirectoryEntry, FileSystemFileEntry, NgxFileDropEntry } from 'ngx-file-drop';
import { NgxSpinnerService } from 'ngx-spinner';
import { first } from 'rxjs/operators';
import { LecturerService } from 'src/app/modules/lecturers/services/lecturer.service';
import { Course } from '../../models/courses.model';
import { CoursesService } from '../../services/courses.service';

@Component({
  selector: 'app-course-import-from-file',
  templateUrl: './course-import-from-file.component.html'
})
export class CourseImportFromFileComponent implements OnInit {

  public files: NgxFileDropEntry[] = [];
  csvRecords: any[] = [];
  header = true;
  error = false;
  loaded = false;
  form: FormGroup;
  defaultDelimiter = ',';
  coursesConflict = false;

  constructor(
    public dialogRef: MatDialogRef<CourseImportFromFileComponent>, 
    private courseService: CoursesService, 
    private lecturerService: LecturerService,
    private spinner: NgxSpinnerService,
    private ngxCsvParser: NgxCsvParser) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      delimiter: new FormControl(this.defaultDelimiter, Validators.required),
    });
  }

  public dropped(files: NgxFileDropEntry[]) {
    this.files = files;
    for (const droppedFile of files) {
      if (droppedFile.fileEntry.isFile) {
        const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
        fileEntry.file((file: File) => {
          this.ngxCsvParser.parse(file, { header: this.header, delimiter: this.form.get('delimiter').value })
          .pipe(first())
          .subscribe((result: Array<any>) => {
            this.csvRecords = result;
            this.csvRecords.forEach(item => {
              item.Accepted = this.getBoolean(item.Accepted);
              item.DepartmentID = Number(item.DepartmentID);
              item.HoursStudent = Number(item.HoursStudent);
              item.HoursUniversity = Number(item.HoursUniversity);
              item.TypeID = Number(item.TypeID);
              item.Year = Number(item.Year);
              item.LanguageTypeID = Number(item.LanguageTypeID);
            })
            this.error = false;
            this.loaded = true;
            this.coursesConflict = false
          }, (error: NgxCSVParserError) => {
            this.error = true
            this.loaded = false;
          });
        });
      }
    }
  }

  exit(): void {
    this.dialogRef.close();
  }

  create() {
    if (this.loaded) {
      this.spinner.show();
      this.courseService.addMultipleCourses(this.csvRecords)
      .pipe(first())
      .subscribe(
        response => {
          this.spinner.hide();
          this.dialogRef.close({success: true});
        },
        (error: HttpErrorResponse) => {
          if (error.status == 409) {
            this.coursesConflict = true;
          } else {
            this.error = true;
          }
        }
      )
    }
  }

  getBoolean(stringValue) {
    if (stringValue === 'true') {
      return true;
    } else {
      return false;
    }
  }
}
