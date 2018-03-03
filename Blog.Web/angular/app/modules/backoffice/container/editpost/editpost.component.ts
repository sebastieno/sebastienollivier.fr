import { PLATFORM_ID, EventEmitter, Output, Input } from '@angular/core';
import { Component, OnInit, Inject } from '@angular/core';
import * as slug from 'slug';
import { Router, ActivatedRoute } from '@angular/router';
import { BackOfficeService } from '../../services/backoffice.service';
import { Post } from '@bw/models';

@Component({
  selector: 'app-editpostcontainer',
  templateUrl: './editpost.component.html',
  styleUrls: ['./editpost.component.scss']
})
export class EditPostComponent implements OnInit {
  post: Post;
  constructor(private route: ActivatedRoute, private router: Router, private backOfficeService: BackOfficeService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.backOfficeService.getPost(params['categoryCode'], params['postUrl']).subscribe(post => {
        this.post = post;
      });
    });
  }

  submit(post: Post) {
    this.backOfficeService.editPost(post).subscribe(() => {
      this.router.navigateByUrl('/back');
    });
  }
}
