using Blog.Data;

namespace Blog.Domain.Entities
{
    public class CategoryWithPostsNumber
    {
        public Category Category { get; set; }
        public int PostsNumber { get; set; }
    }
}
