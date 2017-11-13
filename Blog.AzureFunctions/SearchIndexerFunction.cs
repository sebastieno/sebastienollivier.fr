using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;
using System.Threading.Tasks;
using Blog.Functions.SearchIndexer.BindingRedirectHelper;
using Blog.SearchIndexer;
using System;

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
            log.Info("Launching SearchIndexerFunction");

            try
            {
                var searchIndexer = new Indexer(ConfigurationManager.AppSettings["BlogConnection"], ConfigurationManager.AppSettings["AzureSearchName"], ConfigurationManager.AppSettings["AzureSearchKey"], ConfigurationManager.AppSettings["AzureSearchIndexName"], log);

                await searchIndexer.LaunchIndexation();
            }
            catch (Exception e)
            {
                log.Error("Error during SearchIndexerFunction execution", e);
            }
        }
    }
}
