import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Policy, SecObject } from '../contracts/models/policy';
import { switchMap, catchError, map } from 'rxjs/operators';
import { CommonService } from '../system/service/common.service';
import { HttpClient } from '@angular/common/http';
import { ApplicationContextService } from './application-context.service';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient,
    private appContext: ApplicationContextService
  ) { }

  getAll(): Observable<Policy[]> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<SecObject[]>(`api/${app.AppName}/policy`).pipe(map((secObjects: SecObject[]) => {
        return secObjects.map(so => {
          let policy = new Policy();
          policy.IdPolicy = so.IdSecObject;
          policy.Name = so.ObjectName;
          return policy;
        });
      }), catchError(this.common.handleError("getAll", [])));
    }));    
  }
}
