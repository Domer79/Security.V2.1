import { Injectable } from '@angular/core';
import { CommonService } from '../system/service/common.service';
import { HttpClient } from '@angular/common/http';
import { ApplicationContextService } from './application-context.service';
import { Observable } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CommonSecurityService {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient,
    private appContext: ApplicationContextService
  ) { }

  checkAccess(loginOrEmail: string, policy: string): Observable<boolean>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<boolean>(`api/${app.AppName}/common/checkaccess`, {params: {loginOrEmail, policy}}).pipe(catchError(this.common.handleError("getRoleMembers", false)));
    }));    
  }
}
