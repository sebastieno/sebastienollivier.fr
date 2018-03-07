using Blog.Data;
using Blog.Web.Models;
using System.Collections.Generic;

namespace Blog.Web.Areas.Admin.Models
{
    public class CreatePostModel : PostModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
