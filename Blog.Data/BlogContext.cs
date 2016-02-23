using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Blog.Data
{
    public class BlogContext : DbContext, IBlogContext
    {

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(post =>
            {
                post.HasKey(p => p.Id);
                post.ToTable("Posts");

                post.Ignore(p => p.ComputedDescription);
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
