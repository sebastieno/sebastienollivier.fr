import { PLATFORM_ID, EventEmitter, Output, Input } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import * as slug from 'slug';
import { Router } from '@angular/router';
import { BackOfficeService } from '../../services/backoffice.service';
import { Post } from '@bw/models';

@Component({
  selector: 'app-newpost',
  templateUrl: './newpost.component.html',
  styleUrls: ['./newpost.component.scss']
})
export class NewPostComponent implements OnInit {

  constructor(private router: Router, private backOfficeService: BackOfficeService) { }

  ngOnInit() {
  }

  submit(post: Post) {
    this.backOfficeService.addPost(post).subscribe(() => {
      this.router.navigateByUrl('/back');
    });
  }
}
