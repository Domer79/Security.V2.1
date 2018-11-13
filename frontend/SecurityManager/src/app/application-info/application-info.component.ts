import { Component, OnInit } from '@angular/core';
import { ApplicationContextService } from '../services/application-context.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Application } from '../contracts/models/Application';

@Component({
  selector: 'app-info',
  templateUrl: './application-info.component.html',
  styleUrls: ['./application-info.component.scss']
})
export class ApplicationInfoComponent implements OnInit {
  get application$(): Observable<Application>{
    return this.appContext.Application;
  }

  constructor(
    private appContext: ApplicationContextService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(param => {
      this.appContext.Application.subscribe(app => {
        if (app.AppName !== param.application){
          this.appContext.setApplication(param.application);
        }
      });
    });
  }

}
