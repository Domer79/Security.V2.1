import { Injectable } from '@angular/core';
import { HttpInterceptor, 
  HttpRequest, 
  HttpHandler, 
  HttpSentEvent, 
  HttpHeaderResponse, 
  HttpProgressEvent, 
  HttpResponse, 
  HttpUserEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';

@Injectable()
export class CustomInterceptorService implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {
    let request = req.clone({url: `${environment.host}/${req.url}`});
    return next.handle(request);
}
  constructor() { }

}
