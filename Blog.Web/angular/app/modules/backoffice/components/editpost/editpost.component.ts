import { PLATFORM_ID, EventEmitter, Output, Input } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import { MarkdownService } from 'ngx-markdown';
import { BackOfficeService } from '../../services/backoffice.service';
import * as slug from 'slug';
import { Post, Category } from '@bw/models';
import { isPlatformServer } from '@angular/common';

@Component({
  selector: 'app-editpost',
  templateUrl: './editpost.component.html',
  styleUrls: ['./editpost.component.scss']
})
export class EditPostComponent implements OnInit {
  markdown: string;
  id: number;
  html: string;
  date: Date;
  title: string;
  description: string;
  categtories: Category[];
  category: Category;
  isServer: boolean;
  @Output()
  changed = new EventEmitter<Post>();

  @Input()
  post?: Post;

  constructor(private markdownService: MarkdownService, private backOfficeService: BackOfficeService,@Inject(PLATFORM_ID) platformId) {
    this.isServer = isPlatformServer(platformId);
  }

  ngOnInit() {
    this.backOfficeService.getCategories().subscribe(categ => {
      this.categtories = categ;
      if (this.post) {
        this.id = this.post.id;
        this.markdown = this.post.markDownContent;
        this.date = this.post.publicationDate;
        this.category = this.post.category;
        this.title = this.post.title;
        this.description = this.post.description;
      }
    });
  }

  submit() {
    const post: Post = {
      id: this.id,
      categoryId: this.category.id,
      description: this.description,
      content: this.markdownService.compile(this.markdown),
      markDownContent: this.markdown,
      title: this.title,
      publicationDate: this.date,
      url: slug(this.title)
    };
    this.changed.emit(post);
  }
}
