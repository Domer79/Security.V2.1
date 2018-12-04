import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { CommonService } from '../system/service/common.service';
import { catchError, map } from 'rxjs/operators';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  static readonly NotAuthenticated: string = "notauthenticated";

  constructor(
    private common: CommonService,
    private httpClient: HttpClient, 
    private tokenService: TokenService
  ) { }

  checkTokenExpire(token: string): Observable<boolean>{
    return this.httpClient.get<boolean>("api/token/check-expire", {params: {token}}).pipe(catchError(this.common.handleError("checkTokenExpire", false)));
  }

  createToken(loginOrEmail: string, password: string): Observable<string>{
    return this.httpClient.post("api/common/create-token", null, {params: {loginOrEmail, password}}).pipe(map(res => res as string), catchError(this.handleError("createToken", AuthService.NotAuthenticated)));
  }

  handleError<T> (operation = 'operation', result?: T) {
    return (error: HttpErrorResponse): Observable<T> => {
   
      // TODO: send the error to remote logging infrastructure
      console.error(`Error by operation: ${operation}`);
      console.error(error); // log to console instead
      
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  exit(): Promise<void>{
    return this.httpClient.delete("api/token/stop", {params: {token: this.tokenService.token, reason: null}}).pipe(catchError(this.common.handleError("exit", null))).toPromise();
  }

}
