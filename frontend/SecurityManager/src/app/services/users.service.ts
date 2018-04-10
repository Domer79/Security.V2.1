import { Injectable } from '@angular/core';
import { UserRepository } from '../contracts/repositories/user-repository';
import { User } from '../contracts/models/user';
import { CommonService } from '../system/service/common.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UsersService implements UserRepository {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient
  ) { }

  SetStatus(loginOrEmail: string, status: boolean): Promise<void> {
    return this.httpClient.post("api/user/setstatus", {loginOrEmail, status}).pipe(catchError(this.common.handleError)).toPromise();
  }
  createEmpty(prefix: string): Observable<User> {
    return this.httpClient.get<User>("api/user", {params: {prefixForRequired: 'new_user'}}).pipe(catchError(this.common.handleError));
  }
  getByName(name: string): Observable<User> {
    return this.httpClient.get<User>("api/user", {params: {loginOrEmail: name}}).pipe(catchError(this.common.handleError));
  }
  getAll(): Observable<User[]> {
    return this.httpClient.get<User>("api/user").pipe(catchError(this.common.handleError));
  }
  getElement(id: number): Observable<User> {
    return this.httpClient.get<User>("api/user", {params: {id: id.toString()}}).pipe(catchError(this.common.handleError));
  }
  create(object: User): Observable<User> {
    return this.httpClient.post("api/user", object).pipe(catchError(this.common.handleError));
  }
  update(object: User): Promise<void> {
    return this.httpClient.put("api/user", object).pipe(catchError(this.common.handleError)).toPromise();
  }
  remove(object: User): Promise<void> {
    return this.httpClient.delete("api/user", {params: {id: object.IdMember.toString()}}).pipe(catchError(this.common.handleError)).toPromise();
  }
}
