using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Blog.Data;
using Blog.Domain.Queries;
using Blog.Web.Models;
using Microsoft.Data.Entity;
using Blog.Domain.Filters;

namespace Blog.Web.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private IBlogContext context;
        private const int postsPerPage = 10;

        public BlogController(IBlogContext context)
        {
            this.context = context;
        }

        [Route("category/{categoryCode}", Name = "PostsListForCategory")]
        [Route("", Name = "PostsList")]
        public async Task<ActionResult> List(string categoryCode = null, int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }
           
            var query = new GetPostsQuery(this.context).ForCategory(categoryCode).Build();

            var pagesCount = Math.Ceiling((double) query.Count()/postsPerPage);

            var posts = await query.Paginate((page - 1)*postsPerPage, postsPerPage).ToListAsync();

            var model = new PostsListModel
            {
                Posts = posts,
                CurrentPageIndex = page,
                TotalPageNumber = pagesCount
            };

            return View(model);
        }

        [Route("{categoryCode}/{postUrl}", Order = 3)]
        public async Task<ActionResult> Post(string categoryCode, string postUrl)
        {
            var post = await new GetPostQuery(this.context, categoryCode, postUrl).ExecuteAsync();

            if (post == null)
            {
                return new HttpNotFoundResult();
            }

            ViewBag.Title = post.Title;

            return View(post);
        }

        //[ChildActionOnly]
        [Route("categories")]
        [ResponseCache(Duration = 600)]
        //[OutputCache(Duration = 600)]
        public PartialViewResult Categories()
        {
            var categories = new GetCategoriesWithPostsNumberQuery(this.context).Execute().ToList();

            return PartialView(categories);
        }

        [Route("draft/{id}/{postUrl}")]
        public async Task<ActionResult> Post(int id, string postUrl)
        {
            var post = await new GetDraftQuery(this.context, id, postUrl).ExecuteAsync();

            if (post == null)
            {
                return new HttpNotFoundResult();
            }

            post.PublicationDate = new DateTime();

            return View(post);
        }
    }
}