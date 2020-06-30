import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpResponse
} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
import {ResponseModel} from '../api.model';

@Injectable()
export class UnwraperInterceptor implements HttpInterceptor {

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.responseType === 'json') {
      return next.handle(request).pipe(
        map(event => {
            if (event instanceof HttpResponse && ResponseModel.isResponseModel(event.body)) {
              return event.clone({body: event.body.data});
            } else {
              return event;
            }
          }
        )
      );
    } else {
      return next.handle(request);
    }
  }
}
