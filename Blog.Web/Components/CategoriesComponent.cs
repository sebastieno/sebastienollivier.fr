using System.Threading.Tasks;
using Blog.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web
{
    public class CategoriesViewComponent : ViewComponent
    {
        private QueryCommandBuilder queryCommandBuilder;

        public CategoriesViewComponent(QueryCommandBuilder queryCommandBuilder)
        {
            this.queryCommandBuilder = queryCommandBuilder;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var query = this.queryCommandBuilder.Build<GetCategoriesWithPostsNumberQuery>().Build();
            var categories = await query.ToListAsync();

            return View(categories);
        }
    }
}
