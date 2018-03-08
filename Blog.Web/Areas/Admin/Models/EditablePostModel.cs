using Blog.Data;
using System;
using System.Collections.Generic;

namespace Blog.Web.Areas.Admin.Models
{
    public class EditablePostModel
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Markdown { get; set; }

        public DateTime? PublicationDate { get; set; }

        public string CategoryCode { get; set; }

        public string Category { get; set; }

        public string Tags { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public static EditablePostModel FromPost(Post post)
        {
            return new EditablePostModel
            {
                Id = post.Id,
                Url = post.Url,
                Title = post.Title,
                Description = post.ComputedDescription,
                Content = post.Content,
                Markdown = post.Markdown ?? post.Content,
                PublicationDate = post.PublicationDate,
                Category = post.Category.Name,
                CategoryCode = post.Category.Code,
                Tags = string.Join(';', post.Tags)
            };
        }
    }
}
