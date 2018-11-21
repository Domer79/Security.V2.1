import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonService } from '../system/service/common.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient
  ) { }

  checkTokenExpire(token: string): Observable<boolean>{
    return this.httpClient.get<boolean>("api/token/check-expire", {params: {token}}).pipe(catchError(this.common.handleError("createEmpty", false)));
  }

  checkLogin(login: string, password: string): Observable<string>{
    
  }
}
