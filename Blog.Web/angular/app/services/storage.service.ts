import { Injectable } from '@angular/core';
import { CookieAttributes, getJSON, remove, set } from 'js-cookie';
import { NgModule, Inject, PLATFORM_ID } from '@angular/core';
import { REQUEST } from '@nguniversal/aspnetcore-engine';
import { isPlatformBrowser } from '@angular/common';

@Injectable()
export class StorageService {

  constructor(@Inject(PLATFORM_ID) private platformId, @Inject(REQUEST) private req: any) {
  }

  getItem(name: string) {
    if (isPlatformBrowser(this.platformId)) {
      return getJSON(name);
    } else {
      try {
        return JSON.parse(this.req.cookies.find(x => x.key === name).value);
      } catch (err) {
        return (this.req && this.req.cookies) ? this.req.cookies.find(x => x.key === name).value : undefined;
      }
    }
  }

  setItem(name: string, value: any, options?: CookieAttributes) {
    if (isPlatformBrowser(this.platformId)) {
      set(name, value, options);
    }
  }

  removeItem(name, options?: CookieAttributes) {
    if (isPlatformBrowser(this.platformId)) {
      remove(name, options);
    }
  }
}
