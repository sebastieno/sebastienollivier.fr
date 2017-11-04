import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-backoffice',
  templateUrl: './backoffice.component.html',
  styleUrls: ['./backoffice.component.scss']
})
export class BackofficeComponent implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.post('/api/blog', null, { responseType: 'text' }).subscribe();
  }
}
