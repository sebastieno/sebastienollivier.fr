using Blog.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blog.Domain.Queries
{
    public class GetCategoriesQuery
    {
        private readonly IBlogContext context;

        public GetCategoriesQuery(IBlogContext context)
        {
            this.context = context;
        }

        public IQueryable<Category> Build()
        {
            return this.context.Categories.AsQueryable();
        }
    }
}
