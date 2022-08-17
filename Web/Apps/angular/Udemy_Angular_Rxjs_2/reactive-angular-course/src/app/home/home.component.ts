import {Component, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import {Course, sortCoursesBySeqNo} from '../model/course';
import { CoursesService } from '../services/courses.service';



@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private coursesService: CoursesService) {}
  
  beginnerCourses$: Observable<Course[]>;
  advancedCourses$: Observable<Course[]>;

  ngOnInit() {
    this.reloadCourses();
  }

  reloadCourses(){
    const courses$ = this.coursesService.loadAllCourses();
    this.beginnerCourses$ = courses$.pipe(map((courses : Course[]) => courses.filter(course => course.category == "BEGINNER")));
    this.advancedCourses$ = courses$.pipe(map((courses : Course[]) => courses.filter(course => course.category == "ADVANCED")));
  }
}




