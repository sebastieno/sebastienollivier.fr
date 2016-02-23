using Blog.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Blog.Domain.Queries
{
    public class GetPostQuery
    {
        private IBlogContext context;
        private readonly string categoryCode;
        private readonly string postUrl;

        public GetPostQuery(IBlogContext context, string categoryCode, string postUrl)
        {
            this.context = context;
            this.categoryCode = categoryCode;
            this.postUrl = postUrl;
        }

        public async Task<Post> ExecuteAsync()
        {
           return await context.Posts.Include(p => p.Category)
                .Where(p => p.PublicationDate.HasValue)
                .FirstOrDefaultAsync(p => p.Category.Code.ToLower() == this.categoryCode.ToLower() && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
