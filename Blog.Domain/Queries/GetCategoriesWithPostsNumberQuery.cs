using Blog.Data;
using Blog.Domain.Entities;
using System.Linq;

namespace Blog.Domain.Queries
{
    public class GetCategoriesWithPostsNumberQuery
    {
        private IBlogContext context;

        public GetCategoriesWithPostsNumberQuery(IBlogContext context)
        {
            this.context = context;
        }

        public IQueryable<CategoryWithPostsNumber> Execute()
        {
            return context.Categories.Select(c => new CategoryWithPostsNumber { Category = c, PostsNumber = context.Posts.Count(p => p.Category == c) }).Where(c => c.PostsNumber > 0).OrderByDescending(c => c.PostsNumber).ThenBy(c => c.Category.Name);
            ;
        }
    }
}
