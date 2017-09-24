using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public interface IBlogContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Category> Categories { get; set; }

        DbSet<Tag> Tags { get; set; }
    }
}
