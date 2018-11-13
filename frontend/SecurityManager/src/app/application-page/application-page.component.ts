import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../services/application.service';
import { Application } from '../contracts/models/Application';
import { ApplicationContextService } from '../services/application-context.service';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, Router, UrlTree, UrlSegmentGroup, PRIMARY_OUTLET } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-application-page',
  templateUrl: './application-page.component.html',
  styleUrls: ['./application-page.component.scss']
})
export class ApplicationPageComponent implements OnInit {

  applications$: Observable<Application[]>;
  get application$(): Observable<Application>{
    return this.appContext.Application;
  }

  constructor(
    private appService: ApplicationService,
    private appContext: ApplicationContextService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.applications$ = this.appService.getAll();
    // this.application$ = this.appContext.Application;
  }

  selectApp($event): void {
    this.appContext.setApplication($event.target.value);
    // this.application$ = this.appContext.Application;

    let tree: UrlTree = this.router.parseUrl(this.router.url)
    let g: UrlSegmentGroup = tree.root.children[PRIMARY_OUTLET];
    this.router.navigate([`/${$event.target.value}/${g.segments[1]}`]);
  }

  changeDescription(app: Application): void {
    this.appService.saveApp(app).then(() => {
      this.applications$ = this.appService.getAll();
      this.appContext.setApplication(app.AppName);
    });
  }
}
