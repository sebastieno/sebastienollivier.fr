using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogContext : DbContext, IBlogContext
    {
        public BlogContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(post =>
            {
                post.HasKey(p => p.Id);
                post.ToTable("Posts");
                post.Property(p => p.InternalTags).HasColumnName("Tags");

                post.Ignore(p => p.Tags);
            });

            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(c => c.Id);
                category.ToTable("Categories");
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
