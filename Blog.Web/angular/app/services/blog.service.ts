import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import '../models/Post';
import '../models/Category';

@Injectable()
export class BlogService {

  constructor(private http: HttpClient) { }

  getPosts(page?: number): Observable<PostList> {
    return this.http.get<PostList>('http://localhost:5500/api/blog');
  }

  getPost(categoryCode, postUrl): Observable<Post> {
    return this.http.get<Post>(`http://localhost:5500/api/blog/${categoryCode}/${postUrl}`);
  }
}
