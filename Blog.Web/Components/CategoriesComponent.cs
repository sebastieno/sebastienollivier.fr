using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Domain.Queries;
using Microsoft.Data.Entity;

namespace Blog.Web
{
    public class CategoriesViewComponent : ViewComponent
    {
        private IBlogContext context;

        public CategoriesViewComponent(IBlogContext context)
        {
            this.context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await (new GetCategoriesWithPostsNumberQuery(this.context).Build()).ToListAsync();

            return View(categories);
        }
    }
}
