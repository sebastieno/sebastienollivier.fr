import { PLATFORM_ID } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import { MarkdownService } from 'ngx-markdown';

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
  constructor(private markdownService: MarkdownService) { }

  ngOnInit() {
  }
}
