using Blog.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                .Where(p => p.PublicationDate.HasValue)
                .FirstOrDefaultAsync(p => p.Category.Code.ToLower() == categoryCode.ToLower() && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
