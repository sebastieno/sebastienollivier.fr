import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { AutService } from '../services/aut.service';

@Injectable()
export class AutInterceptor implements HttpInterceptor {

  constructor(private autService: AutService) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (this.autService.isAuthenticated) {
        const JWT = `Bearer ${this.autService.token}`;
        console.log(JWT);
        req = req.clone({
          setHeaders: {
            Authorization: JWT,
          },
        });
    }
    return next.handle(req);
  }
}
