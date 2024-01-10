import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError } from 'rxjs';
@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const idToken = localStorage.getItem('id_token');

    if (idToken) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + idToken),
      });

      return next.handle(cloned).pipe(
        catchError((err: HttpErrorResponse) => {
          if (err && (err.status === 401 || err.status === 403)) {
            this.router.navigate(['/unauthorized']);
          }
          throw 'error in the request ' + err.status;
        })
      );
    } else {
      return next.handle(req).pipe(
        catchError((err: HttpErrorResponse) => {
          if (err && (err.status === 401 || err.status === 403)) {
            this.router.navigate(['/unauthorized']);
          }
          throw 'error in the request ' + err.status;
        })
      );
    }
  }
}
