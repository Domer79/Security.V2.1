import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { Observable, of } from 'rxjs';

@Injectable()
export class CommonService {

  constructor() { }

  public handleError<T> (operation = 'operation', result?: T) {
    return (error: HttpErrorResponse): Observable<T> => {
   
      // TODO: send the error to remote logging infrastructure
      console.error(`Error by operation: ${operation}`);
      console.error(error); // log to console instead
      
      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}
