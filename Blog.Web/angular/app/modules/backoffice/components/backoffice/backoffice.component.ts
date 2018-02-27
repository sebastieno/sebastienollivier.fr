import { PLATFORM_ID } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { AutService } from '@bw/services';

@Component({
  selector: 'app-backoffice',
  templateUrl: './backoffice.component.html',
  styleUrls: ['./backoffice.component.scss']
})
export class BackofficeComponent implements OnInit {
  constructor(
    private http: HttpClient,
    private autService: AutService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      if (!this.autService.isAuthenticated) {
        if (this.autService.hasToken) {
          this.autService.renewToken().subscribe(token => {
            this.http.post('http://localhost:5500/api/blog', null, { responseType: 'text' }).subscribe();
          }, err => console.log(err));
        } else {
          this.autService.login();
        }
      } else {
        this.http.post('http://localhost:5500/api/blog', null, { responseType: 'text' }).subscribe();
      }
    }
  }
}
