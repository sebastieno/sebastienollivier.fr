using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;
using System.Threading.Tasks;
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
        public static async Task Run([TimerTrigger("0 0 2 * * *")]TimerInfo myTimer, TraceWriter log)
        {
            var searchIndexer = new Indexer(ConfigurationManager.AppSettings["BlogConnection"], ConfigurationManager.AppSettings["AzureSearchName"], ConfigurationManager.AppSettings["AzureSearchKey"], ConfigurationManager.AppSettings["AzureSearchIndexName"]);
            await searchIndexer.LaunchIndexation();
        }
    }
}
