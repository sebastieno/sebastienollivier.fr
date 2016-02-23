using Blog.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Blog.Domain.Queries
{
    public class GetDraftQuery
    {
        private IBlogContext context;
        private readonly int id;
        private readonly string postUrl;

        public GetDraftQuery(IBlogContext context, int id, string postUrl)
        {
            this.context = context;
            this.id = id;
            this.postUrl = postUrl;
        }

        public async Task<Post> ExecuteAsync()
        {
           return await context.Posts.Include(p => p.Category)
                .Where(p => !p.PublicationDate.HasValue)
                .FirstOrDefaultAsync(p => p.Id == p.Id && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
