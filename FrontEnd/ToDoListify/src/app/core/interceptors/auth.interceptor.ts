import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private cookieService: CookieService) {}

  // Along with each of the Http request, it will try to send the authorization header if it finds one.
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (this.shouldInterceptRequest(request)) {

      const authRequest = request.clone({
        setHeaders: {
          'Authorization': this.cookieService.get('Authorization')
        }
      });
  
      return next.handle(authRequest);
    }
    // Do not send the authheader if there is no addAuth in the query parameter.
    return next.handle(request);
  }

  private shouldInterceptRequest(request: HttpRequest<any>): boolean {
    // check AddAuth=true
    return request.urlWithParams.indexOf('addAuth=true', 0) > -1? true: false;
  }
}
