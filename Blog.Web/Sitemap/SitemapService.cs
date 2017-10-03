using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain;
using Blog.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Blog.Web.Sitemap
{
    public class SitemapService : SitemapGenerator
    {
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<SitemapService> logger;
        private readonly IUrlHelper urlHelper;
        private readonly TimeSpan expirationDuration;
        private QueryCommandBuilder queryCommandBuilder;

        public SitemapService(IDistributedCache distributedCache, ILogger<SitemapService> logger, IUrlHelperFactory urlHelperFactory, IActionContextAccessor actionContextAccessor, QueryCommandBuilder queryCommandBuilder)
        {
            CacheProfile cacheProfile = new CacheProfile { Duration = 86400 };
            this.expirationDuration = TimeSpan.FromSeconds(cacheProfile.Duration.Value);
            this.distributedCache = distributedCache;
            this.logger = logger;
            this.urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            this.queryCommandBuilder = queryCommandBuilder;
        }

        public async Task<string> GetSitemapXml(int? index = null)
        {
            var data = await this.distributedCache.GetStringAsync("Sitemap");
            var sitemapDocuments = JsonConvert.DeserializeObject(data) as List<string>;
            if (sitemapDocuments == null)
            {
                IReadOnlyCollection<SitemapNode> sitemapNodes = this.GetSitemapNodes();
                sitemapDocuments = this.GetSitemapDocuments(sitemapNodes);
                
                var serializedData = JsonConvert.SerializeObject(sitemapDocuments);

                await this.distributedCache.SetStringAsync("Sitemap", serializedData,
                    options: new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = this.expirationDuration
                    });
            }

            if (index.HasValue && ((index < 1) || (index.Value >= sitemapDocuments.Count())))
            {
                return null;
            }

            return sitemapDocuments[index ?? 0];
        }

        protected virtual IReadOnlyCollection<SitemapNode> GetSitemapNodes()
        {
            DateTime now = DateTime.Now;

            // Fixed pages -> Home & Home blog
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(this.urlHelper.Action("Index", "Home", null, "https"))
                {
                    Priority = 1,
                    LastModified = now,
                    Frequency = SitemapFrequency.Always
                },
                new SitemapNode(this.urlHelper.Action("List", "Blog", null, "https"))
                {
                    Priority = 1,
                    LastModified = now,
                    Frequency = SitemapFrequency.Always
                }
            };

            // Categories pages
            var categories = this.queryCommandBuilder.Build<GetCategoriesQuery>().Build().Select(c => c.Code);
            foreach (var categoryCode in categories)
            {
                nodes.Add(new SitemapNode(this.urlHelper.Action("List", "Blog", new { categoryCode = categoryCode }, "https"))
                {
                    Priority = 0.5,
                    LastModified = now,
                    Frequency = SitemapFrequency.Always
                });
            }

            // Posts pages
            var posts = this.queryCommandBuilder.Build<GetPostsQuery>().Build().Select(p => new { Url = p.Url, CategoryCode = p.Category.Code });
            foreach (var post in posts)
            {
                nodes.Add(new SitemapNode(this.urlHelper.Action("Index", "Post", new { categoryCode = post.CategoryCode, postUrl = post.Url }, "https"))
                {
                    Priority = 0.8,
                    LastModified = now,
                    Frequency = SitemapFrequency.Always
                });
            }

            return nodes;
        }

        protected override string GetSitemapUrl(int index)
        {
            return this.urlHelper.Action("SitemapXml", "Seo", null, "https").TrimEnd('/') + "?index=" + index;
        }

        protected override void LogWarning(Exception exception)
        {
            this.logger.LogWarning(exception.Message, exception);
        }
    }
}