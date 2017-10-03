using System;
using System.Runtime.Serialization;

namespace Blog.Web.Sitemap
{
    [Serializable]
    public class SitemapException : Exception
    {
        public SitemapException(string message) : base(message)
        {
        }

        public SitemapException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SitemapException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}