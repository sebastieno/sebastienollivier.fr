import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../environments/environment';

@Injectable()
export class BlogService {

  constructor(private http: HttpClient) { }

  getPosts(page?: number): Observable<PostList> {
    return this.http.get<PostList>(`${environment.apiUrl}/blog`);
  }

  getPost(categoryCode, postUrl): Observable<Post> {
    return this.http.get<Post>(`${environment.apiUrl}/blog/${categoryCode}/${postUrl}`);
  }
}
