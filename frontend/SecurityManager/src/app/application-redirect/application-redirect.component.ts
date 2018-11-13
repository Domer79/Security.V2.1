import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApplicationService } from '../services/application.service';
import { ApplicationContextService } from '../services/application-context.service';

@Component({
  selector: 'app-application-redirect',
  templateUrl: './application-redirect.component.html',
  styleUrls: ['./application-redirect.component.scss']
})
export class ApplicationRedirectComponent implements OnInit {

  constructor(
    private appContext: ApplicationContextService,
    private router: Router
  ) { }

  ngOnInit() {
    this.appContext.Application.subscribe(app => {
      this.router.navigate([`/${app.AppName}`]);
    });
  }

}
