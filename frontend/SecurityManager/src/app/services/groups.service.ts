import { Injectable } from '@angular/core';
import { CommonService } from '../system/service/common.service';
import { HttpClient } from '@angular/common/http';
import { ApplicationContextService } from './application-context.service';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import { Group } from '../contracts/models/group';
import { User } from '../contracts/models/user';

@Injectable()
export class GroupsService {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient,
    private appContext: ApplicationContextService
  ) { }

  createEmpty(prefix: string): Observable<Group> {
    return this.httpClient.get<Group>("api/groups", {params: {prefixForRequired: 'new_group'}}).pipe(catchError(this.common.handleError));
  }
  getByName(name: string): Observable<Group> {
    return this.httpClient.get<Group>("api/groups", {params: {name}}).pipe(catchError(this.common.handleError));
  }
  getAll(): Observable<Group[]> {
    return this.httpClient.get<Group>("api/groups").pipe(catchError(this.common.handleError));
  }
  getElement(id: number): Observable<Group> {
    return this.httpClient.get<Group>("api/groups", {params: {id: id.toString()}}).pipe(catchError(this.common.handleError));
  }
  create(object: Group): Observable<Group> {
    return this.httpClient.post("api/groups", object).pipe(catchError(this.common.handleError));
  }
  update(object: Group): Promise<void> {
    return this.httpClient.put("api/groups", object).pipe(catchError(this.common.handleError)).toPromise();
  }
  remove(object: Group): Promise<void> {
    return this.httpClient.delete("api/groups", {params: {id: object.IdMember.toString()}}).pipe(catchError(this.common.handleError)).toPromise();
  }
  getGroupUsers(group: Group): Observable<User[]>{
    return this.httpClient.get<User>("api/usergroups", {params: {idGroup: group.IdMember.toString()}}).pipe(catchError(this.common.handleError));
  }
  getNonIncludedUsers(group: Group): Observable<User[]>{
    return this.httpClient.get<Group>("api/usergroups/exceptfor", {params: {idGroup: group.IdMember.toString()}}).pipe(catchError(this.common.handleError));
  }
  addUsersToGroup(group: Group, users: User[]): Promise<void>{
    return this.httpClient.put("api/usergroups", users.map(_ => _.IdMember), {params: {idGroup: group.IdMember.toString()}}).pipe(catchError(this.common.handleError)).toPromise();
  }
  deleteGroupsFromUser(user: User, groups: Group[]): Promise<void>{
    let params = new HttpParams();
    params = params.set("idUser", user.IdMember.toString());
    let request = new HttpRequest<any>("DELETE", "api/usergroups", groups.map(_ => _.IdMember), {params: params});
    return this.httpClient.request(request).pipe(catchError(this.common.handleError)).toPromise();
  }
  getUserRoles(user: User): Observable<Role[]>{
    return this.appContext.Application.switchMap(app => {
      return this.httpClient.get<Role>(`api/${app.AppName}/memberroles`, {params: {idMember: user.IdMember.toString()}}).pipe(catchError(this.common.handleError));
    });
  }
  getNonIncludedRoles(user: User): Observable<Role[]>{
    return this.appContext.Application.switchMap(app => {
      return this.httpClient.get<Role>(`api/${app.AppName}/memberroles/except`, {params: {idMember: user.IdMember.toString()}}).pipe(catchError(this.common.handleError));
    });
  }
  addRolesToUser(user: User, roles: Role[]): Promise<void>{
    return this.appContext.Application.switchMap(app => {
      return this.httpClient.put<Role>(`api/${app.AppName}/memberroles`, roles.map(_ => _.IdRole), {params: {idMember: user.IdMember.toString()}}).pipe(catchError(this.common.handleError));
    }).toPromise();
  }
  deleteRolesFromUser(user: User, roles: Role[]): Promise<void>{
    return this.appContext.Application.switchMap(app => {
      let params = new HttpParams();
      params = params.set("idMember", user.IdMember.toString());
      let request = new HttpRequest<any>("DELETE", `api/${app.AppName}/memberroles`, roles.map(_ => _.IdRole), {params: params});

      return this.httpClient.request(request).pipe(catchError(this.common.handleError));
    }).toPromise();
  }
}
