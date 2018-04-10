import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../services/application.service';
import { Application } from '../contracts/models/Application';
import { ApplicationContextService } from '../services/application-context.service';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-application-page',
  templateUrl: './application-page.component.html',
  styleUrls: ['./application-page.component.scss']
})
export class ApplicationPageComponent implements OnInit {

  applications$: Observable<Application[]>;

  constructor(
    private appService: ApplicationService,
    private appContext: ApplicationContextService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.applications$ = this.appService.getAll();
  }

  selectApp($event): void {
    this.appContext.setApplication($event.target.value);
  }

  changeDescription(app: Application): void {
    this.appService.saveApp(app).then(() => {
      this.applications$ = this.appService.getAll();
      this.appContext.setApplication(app.AppName);
    });
  }
}
