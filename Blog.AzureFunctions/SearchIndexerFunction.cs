using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Blog.Functions.SearchIndexer.BindingRedirectHelper;
using Blog.SearchIndexer;

namespace Blog.Functions.SearchIndexer
{
    public static class SearchIndexerFunction
    {
        static SearchIndexerFunction()
        {
            ApplicationHelper.Startup();
        }

        [FunctionName("SearchIndexerFunction")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var searchIndexer = new Indexer(ConfigurationManager.AppSettings["BlogConnection"], ConfigurationManager.AppSettings["AzureSearchName"], ConfigurationManager.AppSettings["AzureSearchKey"], ConfigurationManager.AppSettings["AzureSearchIndexName"]);
            await searchIndexer.LaunchIndexation();
        }
    }
}
