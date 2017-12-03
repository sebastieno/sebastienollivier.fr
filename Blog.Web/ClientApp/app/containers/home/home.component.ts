import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../services/blog.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  pageCount: number;
  currentIndex: number;
  posts: Post[];

  constructor(private blogService: BlogService) { }

  ngOnInit() {
    this.blogService.getPosts().subscribe(x => {
      this.posts = x.posts;
      this.currentIndex = x.currentPageIndex;
      this.pageCount = x.totalPageNumber;
    });
  }

}
