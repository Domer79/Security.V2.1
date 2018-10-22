import { Injectable } from '@angular/core';
import { CommonService } from '../system/service/common.service';
import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';
import { ApplicationContextService } from './application-context.service';
import { catchError, switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Group } from '../contracts/models/group';
import { User } from '../contracts/models/user';
import { Role } from '../contracts/models/role';

@Injectable()
export class GroupsService {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient,
    private appContext: ApplicationContextService
  ) { }

  createEmpty(prefix: string): Observable<Group> {
    return this.httpClient.get<Group>("api/groups", {params: {prefixForRequired: 'new_group'}}).pipe(catchError(this.common.handleError("createEmpty", null)));
  }
  getByName(name: string): Observable<Group> {
    return this.httpClient.get<Group>("api/groups", {params: {name}}).pipe(catchError(this.common.handleError("getByName", null)));
  }
  getAll(): Observable<Group[]> {
    return this.httpClient.get<Group[]>("api/groups").pipe(catchError(this.common.handleError("getAll", [])));
  }
  getElement(id: number): Observable<Group> {
    return this.httpClient.get<Group>("api/groups", {params: {id: id.toString()}}).pipe(catchError(this.common.handleError("getElement", null)));
  }
  create(object: Group): Observable<Group> {
    return this.httpClient.post("api/groups", object).pipe(catchError(this.common.handleError("create", null)));
  }
  update(object: Group): Promise<void> {
    return this.httpClient.put("api/groups", object).pipe(catchError(this.common.handleError("update", null))).toPromise();
  }
  remove(object: Group): Promise<void> {
    return this.httpClient.delete("api/groups", {params: {id: object.IdMember.toString()}}).pipe(catchError(this.common.handleError("remove", null))).toPromise();
  }
  getGroupUsers(group: Group): Observable<User[]>{
    return this.httpClient.get<User[]>("api/usergroups", {params: {idGroup: group.IdMember.toString()}}).pipe(catchError(this.common.handleError("getGroupUsers", [])));
  }
  getNonIncludedUsers(group: Group): Observable<User[]>{
    return this.httpClient.get<User[]>("api/usergroups/exceptfor", {params: {idGroup: group.IdMember.toString()}}).pipe(catchError(this.common.handleError("getNonIncludedUsers", [])));
  }
  addUsersToGroup(group: Group, users: User[]): Promise<void>{
    return this.httpClient.put("api/usergroups", users.map(_ => _.IdMember), {params: {idGroup: group.IdMember.toString()}}).pipe(catchError(this.common.handleError("addUsersToGroup", null))).toPromise();
  }
  deleteUsersFromGroup(group: Group, users: User[]): Promise<void>{
    let params = new HttpParams();
    params = params.set("idGroup", group.IdMember.toString());
    let request = new HttpRequest<any>("DELETE", "api/usergroups", users.map(_ => _.IdMember), {params: params});
    return this.httpClient.request(request).pipe(catchError(this.common.handleError("deleteUsersFromGroup", null))).toPromise();
  }
  getGroupRoles(group: Group): Observable<Role[]>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role[]>(`api/${app.AppName}/memberroles`, {params: {idMember: group.IdMember.toString()}}).pipe(catchError(this.common.handleError("getGroupRoles", [])));
    }));
  }
  getNonIncludedRoles(group: Group): Observable<Role[]>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role[]>(`api/${app.AppName}/memberroles/except`, {params: {idMember: group.IdMember.toString()}}).pipe(catchError(this.common.handleError("getNonIncludedRoles", [])));
    }));
  }
  addRolesToGroup(group: Group, roles: Role[]): Promise<void>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.put<Role>(`api/${app.AppName}/memberroles`, roles.map(_ => _.IdRole), {params: {idMember: group.IdMember.toString()}}).pipe(catchError(this.common.handleError("AddRolesToGroup", null)));
    })).toPromise();
  }
  deleteRolesFromGroup(group: Group, roles: Role[]): Promise<void>{
    return this.appContext.Application.pipe(switchMap(app => {
      let params = new HttpParams();
      params = params.set("idMember", group.IdMember.toString());
      let request = new HttpRequest<any>("DELETE", `api/${app.AppName}/memberroles`, roles.map(_ => _.IdRole), {params: params});

      return this.httpClient.request(request).pipe(catchError(this.common.handleError("deleteRolesFromGroup", null)));
    })).toPromise();
  }
}
