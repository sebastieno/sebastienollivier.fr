using Blog.Domain;
using Blog.Domain.Command;
using Blog.Domain.Queries;
using Blog.Web.Areas.Admin.Models;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
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

        [Route("new")]
        public async Task<IActionResult> Create()
        {
            var categories = await this.queryCommandBuilder.Build<GetCategoriesQuery>().Build().ToListAsync();

            return View(new EditablePostModel
            {
                Categories = categories
            });
        }

        [HttpPost]
        [Route("new")]
        public async Task<IActionResult> Create(EditablePostModel model)
        {
            await this.queryCommandBuilder.Build<AddPostCommand>().ExecuteAsync(new Data.Post
            {
                PublicationDate = model.PublicationDate,
                Content = model.Content,
                Markdown = model.Markdown,
                Description = model.Description,
                Title = model.Title,
                Tags = model.Tags.Split(';'),
                Url = model.Url,
                CategoryId = model.CategoryId
            });

            var categories = await this.queryCommandBuilder.Build<GetCategoriesQuery>().Build().ToListAsync();

            return RedirectToAction("Edit", new { categoryCode = categories.First(c => c.Id == model.CategoryId).Code, postUrl = model.Url });
        }

        [Route("{categoryCode}/{postUrl}")]
        public async Task<IActionResult> Edit(string categoryCode, string postUrl)
        {
            var post = await this.queryCommandBuilder.Build<GetPostQuery>().WithDrafts().ExecuteAsync(categoryCode, postUrl);
            if (post == null)
            {
                return new NotFoundResult();
            }

            var categories = await this.queryCommandBuilder.Build<GetCategoriesQuery>().Build().ToListAsync();

            EditablePostModel model = EditablePostModel.FromPost(post);
            model.Categories = categories;
            return View(model);
        }

        [HttpPost]
        [Route("{categoryCode}/{postUrl}")]
        public async Task<IActionResult> Edit(string categoryCode, string postUrl, EditablePostModel model)
        {
            await this.queryCommandBuilder.Build<EditPostCommand>().ExecuteAsync(new Data.Post
            {
                Id = model.Id,
                PublicationDate = model.PublicationDate,
                Content = model.Content,
                Markdown = model.Markdown,
                Description = model.Description,
                Title = model.Title,
                Tags = model.Tags.Split(';'),
                Url = model.Url,
                CategoryId = model.CategoryId
            });

            var categories = await this.queryCommandBuilder.Build<GetCategoriesQuery>().Build().ToListAsync();

            return RedirectToAction("Edit", new { categoryCode = categories.First(c => c.Id == model.CategoryId).Code, postUrl = model.Url });
        }
    }
}