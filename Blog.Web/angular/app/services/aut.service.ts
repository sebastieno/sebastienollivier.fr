import { Injectable } from '@angular/core';
import * as auth0 from 'auth0-js';
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';
import { StorageService } from './storage.service';

const AUTH_CONFIG = {
  clientID: 'Ygg0pdZ-QB74OA-fFj4QVn4OtxhzChfS',
  domain: 'ovent.eu.auth0.com',
  callbackURL: 'http://localhost:5500/back'
};


@Injectable()
export class AutService {

  auth0 = new auth0.WebAuth({
    clientID: AUTH_CONFIG.clientID,
    domain: AUTH_CONFIG.domain,
    responseType: 'token id_token',
    audience: `https://${AUTH_CONFIG.domain}/userinfo`,
    redirectUri: AUTH_CONFIG.callbackURL,
    scope: 'openid'
  });

  constructor(private storageService: StorageService) { }

  public login(): void {
    this.auth0.authorize();
  }

  public handleAuthentication(): Promise<void> {
    return new Promise((res, rej) => {
      this.auth0.parseHash((err, authResult) => {
        if (authResult && authResult.accessToken && authResult.idToken) {
          this.setSession(authResult);
        } else if (err) {
          console.log(err);
          rej(err);
        }
        res();
      });
    });
  }

  private setSession(authResult): void {
    // Set the time that the access token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    this.storageService.setItem('access_token', authResult.accessToken);
    this.storageService.setItem('id_token', authResult.idToken);
    this.storageService.setItem('expires_at', expiresAt);
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    this.storageService.removeItem('access_token');
    this.storageService.removeItem('id_token');
    this.storageService.removeItem('expires_at');
    // Go back to the home route
  }

  public get isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time
    const expiresAt = this.storageService.getItem('expires_at');
    return new Date().getTime() < expiresAt;
  }

  public renewToken(): Observable<string> {
    return Observable.create((observer: Observer<string>) => {
      this.auth0.checkSession({}, (err, res) => {
        if (err) {
          observer.error(err);
        } else {
          observer.next(res);
        }
      });
    });

  }

  public get token(): string {
    return this.isAuthenticated ? this.storageService.getItem('id_token') : null;
  }
}
