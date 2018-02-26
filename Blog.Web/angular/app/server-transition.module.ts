import { DOCUMENT, ɵgetDOM, ɵTRANSITION_ID } from '@angular/platform-browser';
import { APP_BOOTSTRAP_LISTENER, APP_ID, ModuleWithProviders, NgModule } from '@angular/core';


export function removeStyleTags(document: HTMLDocument): Function {
  return () => {
    try {
      if (window) {
        const dom = ɵgetDOM();

        const styles: HTMLElement[] =
          Array.prototype.slice.apply(dom.querySelectorAll(document, 'style[ng-transition]'));

        setTimeout(() => styles.forEach(el => dom.remove(el)), 100);
      }
    } catch (e) { }
  };
}

@NgModule({})
export class ServerTransition {
  public static forRoot({ appId }: { appId: string }): ModuleWithProviders {
    return {
      ngModule: ServerTransition,
      providers: [
        { provide: APP_ID, useValue: appId },
        { provide: ɵTRANSITION_ID, useValue: appId },
        {
          provide: APP_BOOTSTRAP_LISTENER,
          useFactory: removeStyleTags,
          deps: [DOCUMENT],
          multi: true
        }
      ]
    };
  }
}
