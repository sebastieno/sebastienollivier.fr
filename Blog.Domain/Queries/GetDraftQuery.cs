using Blog.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Queries
{
    public class GetDraftQuery
    {
        private IBlogContext context;
        private readonly int id;
        private readonly string postUrl;

        public GetDraftQuery(IBlogContext context)
        {
            this.context = context;
        }

        public async Task<Post> ExecuteAsync(int id, string postUrl)
        {
            return await context.Posts.Include(p => p.Category)
                 .Where(p => !p.PublicationDate.HasValue)
                 .FirstOrDefaultAsync(p => p.Id == p.Id && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
