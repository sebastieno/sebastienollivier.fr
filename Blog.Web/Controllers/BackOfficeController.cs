using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Domain;
using Blog.Domain.Command;
using Blog.Domain.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> AddPost(Data.Post post)
    {
      await this.queryCommandBuilder.Build<AddPostCommand>().ExecuteAsync(post);
      return Ok();
    }
    
    [HttpPost]
    [Route("{postUrl}")]
    public async Task<IActionResult> EditPost(Data.Post post)
    {
      await this.queryCommandBuilder.Build<EditPostCommand>().ExecuteAsync(post);
      return Ok();
    }

    [Route("{postUrl}", Order = 3)]
    [HttpGet]
    public async Task<IActionResult> Post(string categoryCode, string postUrl)
    {
      var post = await this.queryCommandBuilder.Build<GetPostQuery>().WithMarkDown().ExecuteAsync(categoryCode, postUrl);
      if (post == null)
      {
        return new NotFoundResult();
      }

      return Json(post);
    }
  }
}
