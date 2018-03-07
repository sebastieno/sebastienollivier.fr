using Blog.Data;
using Blog.Web.Models;
using System.Collections.Generic;

namespace Blog.Web.Areas.Admin.Models
{
    public class EditPostModel : PostModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public static EditPostModel FromPost(Post post)
        {
            return new EditPostModel
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
                Tags = post.Tags
            };
        }
    }
}
