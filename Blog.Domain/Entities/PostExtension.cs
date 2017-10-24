using Blog.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Domain.Entities
{
    public static class PostExtension
    {
        public static Expression<Func<Post, bool>> IsPublishedExpression = post => post.PublicationDate.HasValue && post.PublicationDate < DateTime.Now;

        public static IQueryable<Post> PublishedOnly(this IQueryable<Post> queryable)
        {
            return queryable.Where(IsPublishedExpression);
        }
    }
}
