import { Injectable } from '@angular/core';
import { ApplicationContext } from '../contracts/ApplicationContext';
import { Application } from '../contracts/models/Application';
import { ApplicationService } from './application.service';
import { Observable } from 'rxjs/Observable';
import { ActivatedRoute, UrlSegment, Router } from '@angular/router';
import { switchMap } from 'rxjs/operator/switchMap';
import { map } from 'rxjs/operators';

@Injectable()
export class ApplicationContextService implements ApplicationContext {

  private _application: Observable<Application>;

  constructor(
    private appService: ApplicationService,
    private route: ActivatedRoute,
    private router: Router
  ) { 
  }

  private currentAppName: string = 'SecurityManager';
  
  public get Application() : Observable<Application> {
    let appNameFromUrl = this.getApplicationFromUrl() || this.currentAppName;
    if (this.currentAppName != appNameFromUrl){
      this.setApplication(appNameFromUrl);
    }

    return this._application || (this._application = this.appService.getByName(this.currentAppName));
  }
  
  setApplication(appName: string): void{
    this.currentAppName = appName;
    this._application = this.appService.getByName(appName);
  }

  private getApplicationFromUrl(): string{
    var activatedRoute = this.route.children[0];
    var segments: Observable<UrlSegment[]>[] = [];
    var paths: string[] = [];
    while (activatedRoute != null){
      let segment = activatedRoute.url;
      segments.push(segment)
      activatedRoute = activatedRoute.children[0];
    }

    segments.forEach(element => {
      element.subscribe(seg => {
        if (seg.length == 1)
          if (paths.indexOf(seg[0].path) === -1)
            paths.push(seg[0].path)
      });
    });
    
    return paths[0];
  }
}
