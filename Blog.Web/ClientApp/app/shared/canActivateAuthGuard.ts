import { isPlatformBrowser } from "@angular/common";
import { AuthenticationContext } from "adal-ts";
import { Injectable, Inject, PLATFORM_ID } from "@angular/core";
import { CanActivate } from "@angular/router";
@Injectable()
export class CanActivateViaAuthGuard implements CanActivate {
  constructor(
    private authContext: AuthenticationContext,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  canActivate() {
    if (isPlatformBrowser(this.platformId) && !this.authContext.getUser()) {
      this.authContext.login();
    }
    return true;
  }
}
