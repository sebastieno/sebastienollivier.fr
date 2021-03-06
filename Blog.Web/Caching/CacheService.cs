﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blog.Web.Caching
{
    public class CacheService
    {
        private readonly IMemoryCache cache;

        public CacheService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public async Task<HttpResponseMessage> RenewEntry(string url, string host)
        {
            cache.Remove(url);
            var targetUri = new Uri(new Uri(host), url).ToString();

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(targetUri.ToString());
                return result;
            }
        }
    }
}
