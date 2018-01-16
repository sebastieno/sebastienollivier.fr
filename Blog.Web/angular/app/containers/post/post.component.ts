import { Component, OnInit, Optional, Inject, PLATFORM_ID } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogService } from '../../services/blog.service';
import { Meta } from '@angular/platform-browser';
import { Title } from '@angular/platform-browser';
import { isPlatformBrowser } from '@angular/common';
import * as Prism from 'prismjs';
import 'prismjs/components/prism-csharp';
import 'prismjs/components/prism-json';
import { EventReplayer } from 'preboot';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  post: Post;

  constructor
    (
    private router: Router,
    private route: ActivatedRoute,
    private blogService: BlogService,
    private meta: Meta,
    private title: Title,
    private replayer: EventReplayer,
    @Inject(PLATFORM_ID) private platformId
    ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.blogService.getPost(params['categoryCode'], params['postUrl']).subscribe(post => {
        this.post = post;
        this.meta.addTag({
          name: 'description',
          content: this.post.description
        });
        this.title.setTitle(this.post.title + ' - Blog de William Klein');
        if (isPlatformBrowser(this.platformId)) {
          setTimeout(() => {
            Prism.highlightAll();
            this.replayer.replayAll();
          });
        }
      });
    });
  }

  back() {
    this.router.navigate(['home']);
  }
}
