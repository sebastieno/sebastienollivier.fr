using System;


namespace Blog.Web.Sitemap
{
    public class SitemapNode
    {
        public ChangeFrequency? ChangeFrequency { get; set; }
        public DateTime? Modified { get; set; }
        public double? Priority { get; set; }
        public string Url { get; set; }
    }
}