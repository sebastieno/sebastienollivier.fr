using Blog.Domain;
using Blog.Domain.Command;
using Blog.Domain.Queries;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class HomeController : Controller
    {
        private readonly QueryCommandBuilder queryCommandBuilder;

        public HomeController(QueryCommandBuilder queryCommandBuilder)
        {
            this.queryCommandBuilder = queryCommandBuilder;
        }

        [Route("")]
        public async Task<IActionResult> List()
        {
            var query = this.queryCommandBuilder.Build<GetPostsQuery>().WithDrafts().Build().OrderByDescending(p => p.PublicationDate ?? DateTime.MaxValue);

            return View((await query.ToListAsync()).Select(PostModel.FromPost));
        }

        [Route("{categoryCode}/{postUrl}")]
        public async Task<IActionResult> Edit(string categoryCode, string postUrl)
        {
            var post = await this.queryCommandBuilder.Build<GetPostQuery>().WithDrafts().ExecuteAsync(categoryCode, postUrl);
            if (post == null)
            {
                return new NotFoundResult();
            }

            return View(PostModel.FromPost(post));
        }

        [HttpPost]
        [Route("{categoryCode}/{postUrl}")]
        public async Task<IActionResult> Edit(string categoryCode, string postUrl, PostModel model)
        {
            await this.queryCommandBuilder.Build<EditPostCommand>().ExecuteAsync(new Data.Post
            {
                Id = model.Id,
                PublicationDate = null, //model.PublicationDate
                Content = model.Content,
                Markdown = model.Markdown,
                Description = model.Description,
                Title = model.Title,
                Tags = model.Tags,
                Url = model.Url
            });

            return RedirectToAction("Edit", new { categoryCode = model.CategoryCode, postUrl = model.Url });
        }
    }
}