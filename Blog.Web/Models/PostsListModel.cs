using Blog.Data;
using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class PostsListModel : PagerModel
    {
        public IEnumerable<PostModel> Posts { get; set; }
    }
}