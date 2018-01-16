import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-preview',
  templateUrl: './post-preview.component.html',
  styleUrls: ['./post-preview.component.scss']
})
export class PostPreviewComponent implements OnInit {

  @Input()
  post: Post;

  constructor(private router: Router) { }

  ngOnInit() {
  }

  goToPost() {
    this.router.navigate(['/posts/', this.post.category.code, this.post.url]);
  }
}
