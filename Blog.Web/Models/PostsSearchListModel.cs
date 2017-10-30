using Blog.Data;
using System.Collections.Generic;

namespace Blog.Web.Models
{
    public class PostsSearchListModel : PostsListModel
    {
        public string Search { get; set; }
    }
}