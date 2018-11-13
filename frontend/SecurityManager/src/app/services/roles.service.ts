import { Injectable } from '@angular/core';
import { CommonService } from '../system/service/common.service';
import { HttpClient, HttpParams, HttpRequest } from '@angular/common/http';
import { ApplicationContextService } from './application-context.service';
import { Role } from '../contracts/models/role';
import { Observable } from 'rxjs';
import { catchError, switchMap, map } from 'rxjs/operators';
import { Member } from '../contracts/models/member';
import { Policy, SecObject } from '../contracts/models/policy';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  constructor(
    private common: CommonService,
    private httpClient: HttpClient,
    private appContext: ApplicationContextService
  ) { }

  createEmpty(prefix: string = null): Observable<Role> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role>(`api/${app.AppName}/roles`, {params: {prefix: prefix || 'new_role'}}).pipe(catchError(this.common.handleError("createEmpty", null)));
    }));
  }
  getByName(name: string): Observable<Role> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role>(`api/${app.AppName}/roles`, {params: {name}}).pipe(catchError(this.common.handleError("getByName", null)));
    }));    
  }  
  getAll(): Observable<Role[]> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role[]>(`api/${app.AppName}/roles`).pipe(catchError(this.common.handleError("getAll", [])));
    }));    
  }
  getElement(id: number): Observable<Role> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Role>(`api/${app.AppName}/roles`, {params: {id: id.toString()}}).pipe(catchError(this.common.handleError("getElement", null)));
    }));    
  }
  create(object: Role): Observable<Role> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.post(`api/${app.AppName}/roles`, object).pipe(catchError(this.common.handleError("create", null)));
    }));    
  }
  update(object: Role): Promise<void> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.put(`api/${app.AppName}/roles`, object).pipe(catchError(this.common.handleError("update", null)));
    })).toPromise();    
  }
  remove(object: Role): Promise<void> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.delete(`api/${app.AppName}/groups`, {params: {id: object.IdRole.toString()}}).pipe(catchError(this.common.handleError("remove", null)));
    })).toPromise();    
  }
  getRoleMembers(role: Role): Observable<Member[]>{
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<Member[]>(`api/${app.AppName}/memberroles`, {params: {idRole: role.IdRole.toString()}}).pipe(catchError(this.common.handleError("getRoleMembers", [])));
    }));    
  }
  getPolicies(role: Role): Observable<Policy[]> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<SecObject[]>(`api/${app.AppName}/grants`, {params: {role: role.Name}}).pipe(map((secObjects: SecObject[]) => {
        return secObjects.map(so => {
          let policy = new Policy();
          policy.IdPolicy = so.IdSecObject;
          policy.Name = so.ObjectName;
          return policy;
        });
      }), catchError(this.common.handleError("getPolicies", [])));
    }));    
  }
  deletePoliciesFromRole(role: Role, policies: Policy[]): Promise<void> {
    return this.appContext.Application.pipe(switchMap(app => {
      let params = new HttpParams();
      params = params.set("role", role.Name);
      let request = new HttpRequest<any>("DELETE", `api/${app.AppName}/grants`, policies.map(_ => _.Name), {params: params});
      return this.httpClient.request(request).pipe(catchError(this.common.handleError("deletePoliciesFromRole", null)));
    })).toPromise();    
  }
  setGrants(role: Role, policies: Policy[]): Promise<void> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.put(`api/${app.AppName}/grants`, policies.map(_ => _.Name), {params: {role: role.Name}}).pipe(catchError(this.common.handleError("setGrants", null)));
    })).toPromise();    
  }
  getNonIncludedPolicies(role: Role): Observable<Policy[]> {
    return this.appContext.Application.pipe(switchMap(app => {
      return this.httpClient.get<SecObject[]>(`api/${app.AppName}/grants/except`, {params: {role: role.Name}}).pipe(map((secObjects: SecObject[]) => {
        return secObjects.map(so => {
          let policy = new Policy();
          policy.IdPolicy = so.IdSecObject;
          policy.Name = so.ObjectName;
          return policy;
        });
      }), catchError(this.common.handleError("getNonIncludedPolicies", [])));
    }));
  }
}
