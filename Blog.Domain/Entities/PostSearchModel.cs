using Blog.Data;
using Microsoft.Azure.Search.Models;
using System;
using System.Linq;

namespace Blog.Domain.Entities
{
    [SerializePropertyNamesAsCamelCase]
    public class PostSearchModel
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public DateTime PublicationDate { get; set; }
        public string[] Tags { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public static PostSearchModel FromPost(Post post)
        {
            return new PostSearchModel
            {
                Id = post.Id.ToString(),
                Category = post.Category.Name,
                CategoryCode = post.Category.Code,
                Url = post.Url,
                Content = post.Content,
                Description = post.Description,
                PublicationDate = post.PublicationDate.GetValueOrDefault(),
                Tags = post.Tags.ToArray(),
                Title = post.Title
            };
        }
    }
}
