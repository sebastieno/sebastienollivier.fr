using Blog.Data;
using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class PostsListModel
    {
        public IEnumerable<PostModel> Posts { get; set; }

        public int CurrentPageIndex { get; set; }

        public double TotalPageNumber { get; set; }
    }
}