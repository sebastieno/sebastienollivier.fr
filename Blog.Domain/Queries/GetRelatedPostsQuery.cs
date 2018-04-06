using Blog.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Domain.Entities;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Blog.Domain.Queries
{
    public class GetRelatedPostsQuery
    {
        private IBlogContext context;

        public GetRelatedPostsQuery(IBlogContext context)
        {
            this.context = context;
        }
        
        public IQueryable<Post> Build(string categoryCode, string postUrl)
        {
            return context.Posts.Include(p => p.Category).PublishedOnly()
                .Where(p => p.Category.Code != categoryCode || p.Url != postUrl)
                .OrderByDescending(p => p.Category.Code == categoryCode ? true : false).ThenByDescending(p => p.PublicationDate)
                .Take(4);
        }
    }
}
