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

        public IQueryable<CategoryWithPostsNumber> Build()
        {
            return context.Categories.Select(c => new CategoryWithPostsNumber { Code = c.Code, Name = c.Name, PostsNumber = c.Posts.Count }).OrderByDescending(c => c.PostsNumber).ThenBy(c => c.Name);
        }
    }
}
