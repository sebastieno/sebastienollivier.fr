import { PLATFORM_ID } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import { MarkdownService } from 'ngx-markdown';
import { BackOfficeService } from '../../services/backoffice.service';
import * as slug from 'slug';
import { Router } from '@angular/router';

@Component({
  selector: 'app-newpost',
  templateUrl: './newpost.component.html',
  styleUrls: ['./newpost.component.scss']
})
export class NewPostComponent implements OnInit {
  markdown: string;
  html: string;
  date: Date;
  title: string;
  description: string;
  categtories: Category[];
  category: Category;

  constructor(private router: Router, private markdownService: MarkdownService, private backOfficeService: BackOfficeService) { }

  ngOnInit() {
    this.backOfficeService.getCategories().subscribe(categ => {
      this.categtories = categ;
    });
  }

  post() {
    const post: Post = {
      categoryId: this.category.id,
      description: this.description,
      content: this.markdownService.compile(this.markdown),
      markdownContent: this.markdown,
      title: this.title,
      publicationDate: this.date,
      url: slug(this.title)
    };
    this.backOfficeService.addPost(post).subscribe(() => {
      this.router.navigateByUrl(`/posts/${this.category.code}/${post.url}`);
    });
  }
}
