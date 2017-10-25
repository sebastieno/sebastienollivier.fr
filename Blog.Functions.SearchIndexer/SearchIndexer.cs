using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;
using System.Threading.Tasks;
using Blog.Data; 
using Blog.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using Blog.Functions.SearchIndexer.BindingRedirectHelper;

namespace Blog.Functions.SearchIndexer
{
    public static class SearchIndexer
    {
        static SearchIndexer()
        {
            ApplicationHelper.Startup();
        }

        [FunctionName("SearchIndexer")]
        public static async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["BlogConnection"]);

            var dbContext = new BlogContext(optionsBuilder.Options);

            var posts = await new GetPostsQuery(dbContext).Build().ToListAsync();

            log.Info($"{posts.Count} posts found");
        }
    }
}
