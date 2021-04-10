import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocalStorageService } from 'src/app/services/local-storage.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private storageService: LocalStorageService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const userToken = this.storageService.getUserToken();
    if(userToken) {
      const authReq = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${userToken}`)
      });
      return next.handle(authReq);
    }
    else {
      return next.handle(request);
    }
  }
}
