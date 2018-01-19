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

        public static IQueryable<Post> WithoutContent(this IQueryable<Post> queryable)
        {
            return queryable.Select(x => new Post
            {
                Category = x.Category,
                Description = x.Description,
                Url = x.Url,
                Id = x.Id,
                PublicationDate = x.PublicationDate,
                Tags = x.Tags,
                Title = x.Title
            });
        }



    }
}
