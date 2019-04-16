import { Injectable } from '@angular/core';
import { UserRepository } from '../contracts/repositories/user-repository';
import { User } from '../contracts/models/user';
import { CommonService } from '../system/service/common.service';
import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';
import { catchError, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import { Group } from '../contracts/models/group';
import { Role } from '../contracts/models/role';
import { ApplicationContextService } from './application-context.service';

@Injectable()
export class UsersService implements UserRepository {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient,
    private appContext: ApplicationContextService
  ) { }

  SetStatus(loginOrEmail: string, status: boolean): Promise<void> {
    return this.httpClient.post("api/user/setstatus", {loginOrEmail, status}).pipe(catchError(this.common.handleError("SetStatus", null))).toPromise();
  }
  setPassword(loginOrEmail: string, password: string): Promise<void>{
    return this.httpClient.post("api/common/setpassword", null, {params: {loginOrEmail, password}}).pipe(catchError(this.common.handleError("setPassword", null))).toPromise();
  }
  createEmpty(prefix: string = null): Observable<User> {
    return this.httpClient.get<User>("api/user", {params: {prefixForRequired: prefix || 'new_user'}}).pipe(catchError(this.common.handleError("createEmpty", null)));
  }
  getByName(name: string): Observable<User> {
    return this.httpClient.get<User>("api/user", {params: {loginOrEmail: name}}).pipe(catchError(this.common.handleError("getByName", null)));
  }
  getAll(): Observable<User[]> {
    return this.httpClient.get<User[]>("api/user").pipe(catchError(this.common.handleError("getAll", [])));
  }
  getElement(id: number): Observable<User> {
    return this.httpClient.get<User>("api/user", {params: {id: id.toString()}}).pipe(catchError(this.common.handleError("getElement", null)));
  }
  create(object: User): Observable<User> {
    return this.httpClient.post("api/user", object).pipe(catchError(this.common.handleError("create", null)));
  }
  update(object: User): Promise<void> {
    return this.httpClient.put("api/user", object).pipe(catchError(this.common.handleError("update", null))).toPromise();
  }
  remove(object: User): Promise<void> {
    return this.httpClient.delete("api/user", {params: {id: object.IdMember.toString()}}).pipe(catchError(this.common.handleError("remove", null))).toPromise();
  }
  getUserGroups(user: User): Observable<Group[]>{
    return this.httpClient.get<Group[]>("api/usergroups", {params: {idUser: user.IdMember.toString()}}).pipe(catchError(this.common.handleError("getUserGroups", [])));
  }
  getNonIncludedGroups(user: User): Observable<Group[]>{
    return this.httpClient.get<Group[]>("api/usergroups/exceptfor", {params: {idUser: user.IdMember.toString()}}).pipe(catchError(this.common.handleError("getNonIncludedGroups", [])));
  }
  addGroupsToUser(user: User, groups: Group[]): Promise<void>{
    return this.httpClient.put("api/usergroups", groups.map(_ => _.IdMember), {params: {idUser: user.IdMember.toString()}}).pipe(catchError(this.common.handleError("addGroupsToUser", null))).toPromise();
  }
  deleteGroupsFromUser(user: User, groups: Group[]): Promise<void>{
    let params = new HttpParams();
    params = params.set("idUser", user.IdMember.toString());
    let request = new HttpRequest<any>("DELETE", "api/usergroups", groups.map(_ => _.IdMember), {params: params});
    return this.httpClient.request(request).pipe(catchError(this.common.handleError("deleteGroupsFromUser", null))).toPromise();
  }
  getUserRoles(user: User): Observable<Role[]>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role[]>(`api/${app.AppName}/memberroles`, {params: {idMember: user.IdMember.toString()}}).pipe(catchError(this.common.handleError("getUserRoles", [])));
    }));
  }
  getNonIncludedRoles(user: User): Observable<Role[]>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role[]>(`api/${app.AppName}/memberroles/except`, {params: {idMember: user.IdMember.toString()}}).pipe(catchError(this.common.handleError("getNonIncludedRoles", [])));
    }));
  }
  addRolesToUser(user: User, roles: Role[]): Promise<void>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.put<Role>(`api/${app.AppName}/memberroles`, roles.map(_ => _.IdRole), {params: {idMember: user.IdMember.toString()}}).pipe(catchError(this.common.handleError("addRolesToUser", null)));
    })).toPromise();
  }
  deleteRolesFromUser(user: User, roles: Role[]): Promise<void>{
    return this.appContext.Application.pipe(switchMap(app => {
      let params = new HttpParams();
      params = params.set("idMember", user.IdMember.toString());
      let request = new HttpRequest<any>("DELETE", `api/${app.AppName}/memberroles`, roles.map(_ => _.IdRole), {params: params});

      return this.httpClient.request(request).pipe(catchError(this.common.handleError("deleteRolesFromUser", null)));
    })).toPromise();
  }
}
