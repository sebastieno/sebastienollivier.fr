using Blog.Data;

namespace Blog.Domain.Entities
{
    public class CategoryWithPostsNumber
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int PostsNumber { get; set; }
    }
}
