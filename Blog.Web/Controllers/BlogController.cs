using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain.Queries;
using Blog.Web.Models;
using Blog.Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
  [Route("api/blog")]
  public class BlogController : Controller
  {
    private QueryCommandBuilder queryCommandBuilder;
    private const int postsPerPage = 10;

    public BlogController(QueryCommandBuilder queryCommandBuilder)
    {
      this.queryCommandBuilder = queryCommandBuilder;
    }

    [Route("category/{categoryCode}", Name = "PostsListForCategory")]
    [Route("", Name = "PostsList")]
    public async Task<IActionResult> List(string categoryCode = null, int page = 1)
    {
      if (page < 1)
      {
        page = 1;
      }

      var query = this.queryCommandBuilder.Build<GetPostsQuery>().ForCategory(categoryCode).Build();
      var pagesCount = Math.Ceiling((double)query.Count() / postsPerPage);

      var posts = await query.Paginate((page - 1) * postsPerPage, postsPerPage).ToListAsync();

      var model = new PostsListModel
      {
        Posts = posts,
        CurrentPageIndex = page,
        TotalPageNumber = pagesCount
      };

      return Json(model);
    }

    [Route("{categoryCode}/{postUrl}", Order = 3)]
    public async Task<IActionResult> Post(string categoryCode, string postUrl)
    {
      var post = await this.queryCommandBuilder.Build<GetPostQuery>().ExecuteAsync(categoryCode, postUrl);
      if (post == null)
      {
        return new NotFoundResult();
      }

      return Json(post);
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

      return Json(post);
    }
  }
}
