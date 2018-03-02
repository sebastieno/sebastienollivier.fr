import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../../environments/environment';

@Injectable()
export class BackOfficeService {

  constructor(private http: HttpClient) { }

  getAllPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(`${environment.apiUrl}/backoffice/posts`);
  }

  getPost(categoryCode, postUrl): Observable<Post> {
    return this.http.get<Post>(`${environment.apiUrl}/backoffice/${categoryCode}/${postUrl}`);
  }

  editPost(post: Post): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/backoffice/${post.url}`, post);
  }

  addPost(post: Post): Observable<void> {
    return this.http.post<void>(`${environment.apiUrl}/backoffice`, post);
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${environment.apiUrl}/blog/categories`);
  }
}
