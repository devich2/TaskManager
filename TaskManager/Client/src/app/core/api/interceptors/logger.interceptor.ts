import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';

@Injectable()
export class LoggingInterceptor implements HttpInterceptor {

  private static logResponse(response: HttpResponse<unknown>): void {
    console.log('%c' + response.status + ':', 'color:red', response.url);
    if (response.body) {
      console.log('%cresponse-body:', 'color:red', response.body);
    }
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.logRequest(request);
    return next.handle(request).pipe(
      tap(event => {
        if (event instanceof HttpResponse) {
          LoggingInterceptor.logResponse(event);
        }
      })
    );
  }

  private logRequest(request: HttpRequest<any>): void {
    let params = '';
    if (request.params.keys().length > 0) {
      params = '?' + request.params.keys().map(k => k + '=' + request.params.get(k)).join('&');
    }
    console.log('%c' + request.method + ':', 'color:red', request.url, params);
    if (request.body) {
      console.log('%crequest-body:', 'color:red', request.body);
    }
  }


}
