import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../contracts/models/user';
import { HttpClient } from '@angular/common/http';
import { switchMap, catchError } from 'rxjs/operators';
import { CommonService } from '../system/service/common.service';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor(
    private httpClient: HttpClient,
    private common: CommonService
  ) { }

  get token(): string{
    return window.localStorage.getItem("token")
  }

  setToken(token: string): void{
    window.localStorage.setItem("token", token);
  }

  getUser(): Observable<User>{
    return this.httpClient.get<User>("api/token/get-user", {params: {token: this.token}}).pipe(catchError(this.common.handleError("getUser", null)));
  }
}
