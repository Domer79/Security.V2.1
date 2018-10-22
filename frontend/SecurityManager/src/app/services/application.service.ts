import { Injectable } from '@angular/core';
import { CommonService } from '../system/service/common.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import { Application } from '../contracts/models/Application';

@Injectable()
export class ApplicationService {

  constructor(
    private common: CommonService,
    private httpClient: HttpClient
  ) { }

  getAll(): Observable<Application[]>{
    return this.httpClient.get<Application[]>("api/applications").pipe(catchError(this.common.handleError("getAll", [])));
  }

  getApp(id: number): Observable<Application>{
    return this.httpClient.get<Application>("api/applications", {params: {id: id.toString()}}).pipe(catchError(this.common.handleError("getApp", null)));
  }

  getByName(appName: string): Observable<Application>{
    console.log("Call ApplicationService.getbyName function");
    return this.httpClient.get<Application>("api/applications", {params: {name: appName}}).pipe(catchError(this.common.handleError("getByName", null)));
  }

  saveApp(app: Application): Promise<void>{
    return this.httpClient.put("api/applications", app).pipe(catchError(this.common.handleError("saveApp", null))).toPromise();
  }
}
