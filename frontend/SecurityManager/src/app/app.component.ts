import { Component, OnInit, ElementRef } from '@angular/core';
import { UsersService } from './services/users.service';
import { User } from './contracts/models/user';
import * as moment from 'moment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  constructor(
    
  ){ 
  }

  ngOnInit(): void {
  }
  
  momentDateFormat(date: Date, format: string){
    return moment(date).format(format);
  }

}
