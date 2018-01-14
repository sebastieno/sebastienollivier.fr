import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogService } from '../../services/blog.service';
import { Meta } from '@angular/platform-browser';
import { Title } from '@angular/platform-browser';
import { Location } from '@angular/common';
import * as Prism from 'prismjs';
import 'prismjs/components/prism-csharp';
import 'prismjs/components/prism-json';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
  post: Post;

  constructor(private location: Location, private route: ActivatedRoute, private blogService: BlogService, private meta: Meta, private title: Title) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.blogService.getPost(params['categoryCode'], params['postUrl']).subscribe(post => {
        this.post = post;
        this.meta.addTag({
          name: 'description',
          content: this.post.description
        });
        this.title.setTitle(this.post.title + ' - Blog de William Klein');
        setTimeout(() => Prism.highlightAll());
      });
    });
  }

  back() {
    this.location.back();
  }
}
