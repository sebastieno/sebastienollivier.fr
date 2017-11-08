using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Blog.Domain;
using Blog.Domain.Queries;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Blog.Web.Models;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly QueryCommandBuilder queryCommandBuilder;

        public HomeController(QueryCommandBuilder queryCommandBuilder)
        {
            this.queryCommandBuilder = queryCommandBuilder;
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("toutvapeter")]
        public ActionResult Bug()
        {
            throw new ArgumentException("This is a bug");
        }

        [Route("oops")]
        [Route("oops/{statusCode}")]
        public async Task<ActionResult> Oops(int statusCode = 500)
        {
            if (statusCode == (int)HttpStatusCode.NotFound)
            {
                var query = this.queryCommandBuilder.Build<GetPostsQuery>().Build();
                var post = await query.FirstAsync();

                return View("404", PostModel.FromPost(post));
            }

            return View();
        }
    }
}
