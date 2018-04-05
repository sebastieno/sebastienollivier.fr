using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain.Queries;
using Blog.Web.Models;
using Blog.Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using Blog.Web.Caching;

namespace Blog.Web.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private QueryCommandBuilder queryCommandBuilder;
        private const int postsPerPage = 10;

        public BlogController(QueryCommandBuilder queryCommandBuilder)
        {
            this.queryCommandBuilder = queryCommandBuilder;
        }

        [OuputCacheActionFilter]
        [Route("category/{categoryCode}", Name = "PostsListForCategory")]
        [Route("", Name = "PostsList")]
        public async Task<ActionResult> List(string categoryCode = null, string search = null, int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(search))
            {
                var query = this.queryCommandBuilder.Build<GetPostsQuery>().ForCategory(categoryCode).Build();
                var posts = await query.Paginate((page - 1) * postsPerPage, postsPerPage).ToListAsync();

                var model = new PostsListModel
                {
                    TotalPageNumber = Math.Ceiling((double)query.Count() / postsPerPage),
                    Posts = posts.Select(PostModel.FromPost),
                    CurrentPageIndex = page
                };

                return View(model);
            }
            else
            {
                var searchResult = await this.queryCommandBuilder.Build<GetPostsFromSearchQuery>().Paginate((page - 1) * postsPerPage, postsPerPage).ExecuteAsync(search);

                var model = new PostsSearchListModel
                {
                    Search = search,
                    TotalPageNumber = searchResult.Count.HasValue ? Math.Ceiling((double)searchResult.Count.Value / postsPerPage) : page,
                    Posts = searchResult.Results.Select(r => r.Document).Select(PostModel.FromSearchModel),
                    CurrentPageIndex = page
                };

                return View("SearchList", model);
            }
        }

        [OuputCacheActionFilter]
        [Route("{categoryCode}/{postUrl}", Order = 3)]
        public async Task<ActionResult> Post(string categoryCode, string postUrl)
        {
            var post = await this.queryCommandBuilder.Build<GetPostQuery>().ExecuteAsync(categoryCode, postUrl);
            if (post == null)
            {
                return new NotFoundResult();
            }

            return View(PostModel.FromPost(post));
        }

        [Route("draft/{id}/{postUrl}")]
        public async Task<ActionResult> Post(int id, string postUrl)
        {
            var post = await this.queryCommandBuilder.Build<GetDraftQuery>().ExecuteAsync(id, postUrl);

            if (post == null)
            {
                return new NotFoundResult();
            }

            post.PublicationDate = new DateTime();

            return View(PostModel.FromPost(post));
        }

        [OuputCacheActionFilter]
        [Route("related/{categoryCode}/{postUrl}")]
        public async Task<ActionResult> RelatedPosts(string categoryCode, string postUrl)
        {
            var related  = await this.queryCommandBuilder.Build<GetRelatedPostsQuery>().Build(categoryCode, postUrl).ToListAsync();
            return Json(related.Select(PostModel.FromPost));
        }
    }
}