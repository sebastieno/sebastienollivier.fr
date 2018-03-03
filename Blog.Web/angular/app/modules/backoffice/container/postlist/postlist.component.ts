import { PLATFORM_ID, EventEmitter, Output, Input } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import * as slug from 'slug';
import { Router } from '@angular/router';
import { BackOfficeService } from '../../services/backoffice.service';
import { Post } from '@bw/models';

@Component({
  selector: 'app-postlist',
  templateUrl: './postlist.component.html',
  styleUrls: ['./postlist.component.scss']
})
export class PostListComponent implements OnInit {
  posts: Post[];

  constructor(private router: Router, private backOfficeService: BackOfficeService) { }

  ngOnInit() {
    this.backOfficeService.getAllPosts().subscribe(posts => this.posts = posts);
  }

  goToPost(post: Post) {
    this.router.navigate(['/back/edit/', post.category.code, post.url]);
  }
}
