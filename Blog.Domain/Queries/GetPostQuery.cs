using Blog.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Entities;
using System.Linq;

namespace Blog.Domain.Queries
{
    public class GetPostQuery
    {
        private IBlogContext context;
        private bool publishedOnly = true;

        public GetPostQuery(IBlogContext context)
        {
            this.context = context;
        }

        public GetPostQuery WithDrafts()
        {
            this.publishedOnly = false;
            return this;
        }

        public Task<Post> ExecuteAsync(string categoryCode, string postUrl)
        {
            var query = context.Posts.Include(p => p.Category).AsQueryable();
            if (this.publishedOnly)
            {
                query = query.PublishedOnly();
            }

            return query.FirstOrDefaultAsync(p => p.Category.Code.ToLower() == categoryCode.ToLower() && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
