using System;


namespace Blog.Web.Sitemap
{
    public sealed class SitemapNode
    {
        public SitemapNode(string url)
        {
            this.Url = url;
            this.Priority = null;
            this.Frequency = null;
            this.LastModified = null;
        }

        public SitemapFrequency? Frequency { get; set; }
        public DateTime? LastModified { get; set; }
        public double? Priority { get; set; }
        public string Url { get; set; }
    }
}