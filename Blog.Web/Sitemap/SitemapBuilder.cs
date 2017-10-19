using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Blog.Web.Sitemap
{
    public class SitemapBuilder
    {
        private readonly XNamespace NS = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private readonly List<SitemapNode> nodes = new List<SitemapNode>();

        public void AddUrl(string url, DateTime? modified = null, ChangeFrequency? changeFrequency = null, double? priority = null)
        {
            this.AddUrl(new SitemapNode
            {
                Url = url,
                Modified = modified,
                ChangeFrequency = changeFrequency,
                Priority = priority,
            });
        }

        public void AddUrl(SitemapNode node)
        {
            this.nodes.Add(node);
        }

        public override string ToString()
        {
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(NS + "urlset", this.nodes.Select(CreateItemElement)));

            return sitemap.ToString();
        }

        private XElement CreateItemElement(SitemapNode url)
        {
            var itemElement = new XElement(NS + "url", new XElement(NS + "loc", url.Url.ToLower()));

            if (url.Modified.HasValue)
            {
                itemElement.Add(new XElement(NS + "lastmod", url.Modified.Value.ToString("yyyy-MM-ddTHH:mm:ss.f") + "+00:00"));
            }

            if (url.ChangeFrequency.HasValue)
            {
                itemElement.Add(new XElement(NS + "changefreq", url.ChangeFrequency.Value.ToString().ToLower()));
            }

            if (url.Priority.HasValue)
            {
                itemElement.Add(new XElement(NS + "priority", url.Priority.Value.ToString("N1")));
            }

            return itemElement;
        }
    }
}
