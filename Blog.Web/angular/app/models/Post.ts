import { Category } from "@bw/models";

export interface Post {
  id?: number;
  url: string;
  title: string;
  description: string;
  computedDescription?: string;
  content: string;
  markDownContent: string;
  publicationDate: Date;
  categoryId: number;
  category?: Category;
  tags?: string[];
}
export interface PostList {
  posts: Post[];
  currentPageIndex: number;
  totalPageNumber: number;
}
