import {of} from 'rxjs/observable/of';
import { Injectable } from '@angular/core';
import { ApplicationContext } from '../contracts/ApplicationContext';
import { Application } from '../contracts/models/Application';
import { ApplicationService } from './application.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ApplicationContextService implements ApplicationContext {

  private _application: Observable<Application>;

  constructor(
    private appService: ApplicationService,
  ) { 
  }
  
  public get Application() : Observable<Application> {
    return this._application || (this._application = this.appService.getByName('SecurityManager'));
  }
  
  setApplication(appName: string): void{
    this._application = this.appService.getByName(appName);
  }
}
