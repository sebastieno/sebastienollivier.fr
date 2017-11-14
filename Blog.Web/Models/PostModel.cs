using Blog.Data;
using Blog.Domain.Entities;
using System;

namespace Blog.Web.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public DateTime? PublicationDate { get; set; }

        public string CategoryCode { get; set; }

        public string Category { get; set; }

        public string[] Tags { get; set; }

        public static PostModel FromPost(Post post)
        {
            return new PostModel
            {
                Id = post.Id,
                Url = post.Url,
                Title = post.Title,
                Description = post.ComputedDescription,
                Content = post.Content,
                PublicationDate = post.PublicationDate,
                Category = post.Category.Name,
                CategoryCode = post.Category.Code,
                Tags = post.Tags
            };
        }

        public static PostModel FromSearchModel(PostSearchModel post)
        {
            return new PostModel
            {
                Id = int.Parse(post.Id),
                Url = post.Url,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                PublicationDate = post.PublicationDate,
                Category = post.Category,
                CategoryCode = post.CategoryCode,
                Tags = post.Tags
            };
        }
    }
}
