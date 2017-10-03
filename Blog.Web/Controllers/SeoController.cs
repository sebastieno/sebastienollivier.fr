using System.Text;
using System.Threading.Tasks;
using Blog.Web.Sitemap;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("")]
    public class SeoController : Controller
    {
        private readonly SitemapService sitemapService;

        public SeoController(SitemapService sitemapService)
        {
            this.sitemapService = sitemapService;
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
        public async Task<IActionResult> SitemapXml(int? index = null)
        {
            string content = await this.sitemapService.GetSitemapXml(index);

            if (content == null)
            {
                return BadRequest("Sitemap index is out of range.");
            }

            return Content(content, "application/xml", Encoding.UTF8);
        }
    }
}
