using Blog.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Entities;

namespace Blog.Domain.Queries
{
    public class GetPostQuery
    {
        private IBlogContext context;

        private bool withMarkDown = false;

        public GetPostQuery(IBlogContext context)
        {
            this.context = context;
        }

        public GetPostQuery WithMarkDown()
        {
            this.withMarkDown = true;
            return this;
        }

        public async Task<Post> ExecuteAsync(string categoryCode, string postUrl)
        {
            var query = context.Posts.Include(p => p.Category).PublishedOnly();
            if (!this.withMarkDown)
            {
                query.WithoutMarkDown();
            }
            return await query.FirstOrDefaultAsync(p => p.Category.Code.ToLower() == categoryCode.ToLower() && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
