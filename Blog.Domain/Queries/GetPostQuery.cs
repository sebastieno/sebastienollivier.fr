using Blog.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Entities;

namespace Blog.Domain.Queries
{
    public class GetPostQuery
    {
        private IBlogContext context;

        private bool withUnPublish = false;
        private bool withContent = true;
        private bool withMarkDown = true;
        public GetPostQuery(IBlogContext context)
        {
            this.context = context;
        }

        public GetPostQuery WithoutContent()
        {
            this.withContent = false;
            return this;
        }

        public GetPostQuery WithoutMarkDown()
        {
            this.withMarkDown = false;
            return this;
        }

        public GetPostQuery WithUnpublish()
        {
            this.withUnPublish = true;
            return this;
        }

        public async Task<Post> ExecuteAsync(string categoryCode, string postUrl)
        {
            var query = context.Posts.Include(p => p.Category);

            if (!this.withUnPublish)
            {
                query.PublishedOnly();
            }

            if (!this.withContent)
            {
                query.WithoutContent();
            }

            if (!this.withMarkDown)
            {
                query.WithoutMarkDown();
            }

            return await query.FirstOrDefaultAsync(p => p.Category.Code.ToLower() == categoryCode.ToLower() && p.Url.ToLower() == postUrl.ToLower());
        }
    }
}
