import { Component, OnInit, Inject, PLATFORM_ID } from '@angular/core';
import { BlogService } from '../../services/blog.service';
import { EventReplayer } from 'preboot';
import { isPlatformBrowser } from '@angular/common';

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
    private replayer: EventReplayer,
    @Inject(PLATFORM_ID) private platformId
  ) { }

  ngOnInit() {
    this.blogService.getPosts().subscribe(x => {
      this.posts = x.posts;
      this.currentIndex = x.currentPageIndex;
      this.pageCount = x.totalPageNumber;
      if (isPlatformBrowser(this.platformId)) {
        setTimeout(() => this.replayer.replayAll());
      }
    });
  }

}
