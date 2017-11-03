using Blog.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Entities;

namespace Blog.Domain.Queries
{
    public class GetPostQuery
    {
        private IBlogContext context;

        public GetPostQuery(IBlogContext context)
        {
            this.context = context;
        }

        public async Task<Post> ExecuteAsync(string categoryCode, string postUrl)
        {
           return await context.Posts.Include(p => p.Category)
                .PublishedOnly()
                .FirstOrDefaultAsync(p => p.Category.Code.ToLower() == categoryCode.ToLower() && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
