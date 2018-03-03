import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { EventReplayer } from 'preboot';
import { isPlatformBrowser } from '@angular/common';
import { BlogService } from '@bw/services';
import { Router } from '@angular/router';
import { Post } from '@bw/models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  pageCount: number;
  currentIndex: number;
  posts: Post[];

  constructor(
    private blogService: BlogService,
    private router: Router
  ) { }

  ngOnInit() {
    this.blogService.getPosts().subscribe(x => {
      this.posts = x.posts;
      this.currentIndex = x.currentPageIndex;
      this.pageCount = x.totalPageNumber;
    });
  }

  goToPost(post: Post) {
    this.router.navigate(['/posts/', post.category.code, post.url]);
  }

}
