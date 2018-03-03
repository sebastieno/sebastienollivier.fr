using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain;
using Blog.Domain.Command;
using Blog.Domain.Queries;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
  [Route("api/backoffice")]
  [Authorize(Roles = "Blogger")]
  public class BackOfficeController : Controller
  {
    private QueryCommandBuilder queryCommandBuilder;

    public BackOfficeController(QueryCommandBuilder queryCommandBuilder)
    {
      this.queryCommandBuilder = queryCommandBuilder;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddPost([FromBody]Data.Post post)
    {
      await this.queryCommandBuilder.Build<AddPostCommand>().ExecuteAsync(post);
      return Ok();
    }

    [HttpPost]
    [Route("{postUrl}")]
    public async Task<IActionResult> EditPost([FromBody]Data.Post post)
    {
      await this.queryCommandBuilder.Build<EditPostCommand>().ExecuteAsync(post);
      return Ok();
    }

    [Route("{categoryCode}/{postUrl}", Order = 3)]
    [HttpGet]
    public async Task<IActionResult> Post(string categoryCode, string postUrl)
    {
      var post = await this.queryCommandBuilder.Build<GetPostQuery>().WithUnpublish().WithoutContent().ExecuteAsync(categoryCode, postUrl);
      if (post == null)
      {
        return new NotFoundResult();
      }

      return Json(post);
    }

    [Route("category/{categoryCode}")]
    [Route("posts")]
    [HttpGet]
    public async Task<IActionResult> AllPosts(string categoryCode = null)
    {
      var query = this.queryCommandBuilder.Build<GetPostsQuery>().ForCategory(categoryCode).WithUnpublish().Build();

      var posts = await query.ToListAsync();

      return Json(posts);
    }
  }
}
