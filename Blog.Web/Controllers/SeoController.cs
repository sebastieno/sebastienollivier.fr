using System.Text;
using System.Threading.Tasks;
using Blog.Web.Sitemap;
using Microsoft.AspNetCore.Mvc;
using System;
using Blog.Domain.Queries;
using Blog.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
    [Route("")]
    public class SeoController : Controller
    {
        private readonly SitemapBuilder sitemapBuilder;
        private readonly QueryCommandBuilder queryCommandBuilder;

        public SeoController(SitemapBuilder sitemapBuilder, QueryCommandBuilder queryCommandBuilder)
        {
            this.sitemapBuilder = sitemapBuilder;
            this.queryCommandBuilder = queryCommandBuilder;
        }

        [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)]
        [Route("robots.txt")]
        public ContentResult RobotsText()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("allow: /");
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(Url.Action("SitemapXml", "Seo", null, Request.Scheme).TrimEnd('/'));

            return Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }

        [Route("sitemap.xml")]
        public async Task<IActionResult> SitemapXml()
        {
            var now = DateTime.Now;

            // Fixed pages -> Home & Home blog
            this.sitemapBuilder.AddUrl(new SitemapNode
            {
                Url = this.Url.Action("Index", "Home", null, "https"),
                ChangeFrequency = ChangeFrequency.Always,
                Modified = now,
                Priority = 1
            });

            this.sitemapBuilder.AddUrl(new SitemapNode
            {
                Url = this.Url.Action("List", "Blog", null, "https"),
                Priority = 1,
                Modified = now,
                ChangeFrequency = ChangeFrequency.Always
            });
            // Categories pages
            var categories = await this.queryCommandBuilder.Build<GetCategoriesQuery>().Build().Select(c => c.Code).ToListAsync();
            foreach (var categoryCode in categories)
            {
                this.sitemapBuilder.AddUrl(new SitemapNode
                {
                    Url = this.Url.Action("List", "Blog", new { categoryCode = categoryCode }, "https"),
                    Priority = 0.5,
                    Modified = now,
                    ChangeFrequency = ChangeFrequency.Always
                });
            }

            // Posts pages
            var posts = await this.queryCommandBuilder.Build<GetPostsQuery>().Build().Select(p => new { PostUrl = p.Url, CategoryCode = p.Category.Code, PublicationDate = p.PublicationDate }).ToListAsync();
            foreach (var post in posts)
            {
                this.sitemapBuilder.AddUrl(new SitemapNode
                {
                    Url = this.Url.Action("Post", "Blog", new { categoryCode = post.CategoryCode, postUrl = post.PostUrl }, "https"),
                    Priority = 0.5,
                    Modified = post.PublicationDate,
                    ChangeFrequency = ChangeFrequency.Always
                });
            }

            return Content(this.sitemapBuilder.ToString(), "application/xml", Encoding.UTF8);
        }
    }
}
