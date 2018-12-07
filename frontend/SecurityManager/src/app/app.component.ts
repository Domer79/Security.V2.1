import { Component, OnInit, ElementRef } from '@angular/core';
import { UsersService } from './services/users.service';
import { User } from './contracts/models/user';
import * as moment from 'moment';
import { ActivatedRoute, UrlSegment } from '@angular/router';
import { ApplicationContextService } from './services/application-context.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  constructor(
    private route: ActivatedRoute,
    private appContext: ApplicationContextService
  ){ 
  }

  ngOnInit(): void {
    console.log();
  }
  
  momentDateFormat(date: Date, format: string){
    return moment(date).format(format);
  }

}
