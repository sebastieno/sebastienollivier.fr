import {
  Component,
  OnInit,
  OnDestroy,
  Inject,
  ViewEncapsulation,
  RendererFactory2,
  PLATFORM_ID,
  Injector
} from "@angular/core";
import {
  Router,
  NavigationEnd,
  ActivatedRoute,
  PRIMARY_OUTLET
} from "@angular/router";
import {
  Meta,
  Title,
  DOCUMENT,
  MetaDefinition
} from "@angular/platform-browser";
import { Subscription } from "rxjs/Subscription";
import { isPlatformServer, isPlatformBrowser } from "@angular/common";
import { REQUEST } from "@nguniversal/aspnetcore-engine";
import { Authentication } from "adal-ts";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit, OnDestroy {
  private endPageTitle: string = "Blog de William Klein";
  private defaultPageTitle: string = "Blog de William Klein";

  private routerSub$: Subscription;
  private request;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private title: Title,
    private meta: Meta,
    private injector: Injector,
    @Inject(PLATFORM_ID) private plateformId: Object
  ) {
    this.request = this.injector.get(REQUEST);
    console.log(this.request);
  }

  ngOnInit() {
    this._changeTitleOnNavigation();
    if (isPlatformBrowser(this.plateformId)) {
      Authentication.getAadRedirectProcessor().process();
    }
  }

  ngOnDestroy() {
    // Subscription clean-up
    this.routerSub$.unsubscribe();
  }

  private _changeTitleOnNavigation() {
    this.routerSub$ = this.router.events
      .filter(event => event instanceof NavigationEnd)
      .map(() => this.activatedRoute)
      .map(route => {
        while (route.firstChild) route = route.firstChild;
        return route;
      })
      .filter(route => route.outlet === "primary")
      .mergeMap(route => route.data)
      .subscribe(event => {
        this._setMetaAndLinks(event);
      });
  }

  private _setMetaAndLinks(event) {
    const title = event["title"]
      ? `${event["title"]} - ${this.endPageTitle}`
      : `${this.defaultPageTitle} - ${this.endPageTitle}`;

    this.title.setTitle(title);

    const metaData = event["meta"] || [];
    const linksData = event["links"] || [];

    for (let i = 0; i < metaData.length; i++) {
      this.meta.updateTag(metaData[i]);
    }
  }
}
