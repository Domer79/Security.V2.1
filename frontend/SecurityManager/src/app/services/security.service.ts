import { Injectable } from '@angular/core';
import { Security } from '../contracts/security';
import { UserRepository } from '../contracts/repositories/user-repository';
import { UsersService } from './users.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from './token.service';
import { switchMap, catchError } from 'rxjs/operators';
import { CommonService } from '../system/service/common.service';
import { ApplicationContextService } from './application-context.service';

@Injectable({
  providedIn: 'root'
})
export class SecurityService implements Security {

  constructor(
    private httpClient: HttpClient,
    private tokenService: TokenService,
    private appContext: ApplicationContextService,
    private common: CommonService
  ) { 
    
  }

  checkAccess(policy: string): Observable<boolean> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient
        .get<boolean>(`api/${app.AppName}/common/check-access-token`, {params: {token: this.tokenService.token, policy: policy}})
        .pipe(catchError(this.common.handleError('checkAccess', false)));
    }));
  }

}
