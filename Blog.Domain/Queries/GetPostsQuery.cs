using Blog.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Blog.Domain.Queries
{
    public class GetPostsQuery
    {
        private readonly IBlogContext context;
        private string categoryCode;

        public GetPostsQuery(IBlogContext context)
        {
            this.context = context;
        }

        public GetPostsQuery ForCategory(string categoryCode)
        {
            this.categoryCode = categoryCode;
            return this;
        }

        public IQueryable<Post> Build()
        {
            var query = context.Posts.Include(p => p.Category).Where(p => p.PublicationDate.HasValue);
            if (!string.IsNullOrEmpty(this.categoryCode))
            {
                query = query.Where(p => p.Category.Code.ToLower() == this.categoryCode.ToLower());
            }

            return query.OrderByDescending(p => p.PublicationDate);
        }
    }
}
