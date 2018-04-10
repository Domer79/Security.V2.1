import { Component, OnInit } from '@angular/core';
import { ApplicationContextService } from '../services/application-context.service';

@Component({
  selector: 'app-info',
  templateUrl: './application-info.component.html',
  styleUrls: ['./application-info.component.scss']
})
export class ApplicationInfoComponent implements OnInit {

  constructor(
    private appContext: ApplicationContextService
  ) { }

  ngOnInit() {
  }

}
