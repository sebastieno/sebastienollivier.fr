using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data
{
    public interface IBlogContext
    {
        DbSet<Post> Posts { get; set; }
        DbSet<Category> Categories { get; set; }
    }
}
