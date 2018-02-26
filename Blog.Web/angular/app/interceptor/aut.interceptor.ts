import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AutService } from '../services/aut.service';

@Injectable()
export class AutInterceptor implements HttpInterceptor {

  constructor(private autService: AutService) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (this.autService.isAuthenticated) {
      const JWT = `Bearer ${this.autService.token}`;
      req = req.clone({
        setHeaders: { Authorization: JWT },
      });
    }
    return next.handle(req).catch((err: HttpErrorResponse) => {
      if (err.status === 401) {
        return this.autService.renewToken().mergeMap(token => {
          const JWT = `Bearer ${token.idToken}`;
          req = req.clone({
            setHeaders: { Authorization: JWT },
          });
          return next.handle(req);
        });
      } else {
        throw new Error(err.statusText);
      }
    });
  }
}
