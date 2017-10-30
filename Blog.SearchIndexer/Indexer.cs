using Blog.Data;
using Blog.Domain.Queries;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.SearchIndexer
{
    public class Indexer
    {
        private readonly string IndexName = "posts";

        private readonly string databaseConnectionString;
        private readonly string azureSearchName;
        private readonly string azureSearchKey;

        public Indexer(string databaseConnectionString, string azureSearchName, string azureSearchKey)
        {
            this.databaseConnectionString = databaseConnectionString;
            this.azureSearchName = azureSearchName;
            this.azureSearchKey = azureSearchKey;
        }

        public async Task LaunchIndexation()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer(this.databaseConnectionString);

            IEnumerable<Post> posts = null;
            using (var dbContext = new BlogContext(optionsBuilder.Options))
            {
                posts = await new GetPostsQuery(dbContext).Build().ToListAsync();
            }

            await this.IndexPosts(this.azureSearchName, this.azureSearchKey, posts);
        }

        public async Task IndexPosts(string azureSearchName, string azureSearchKey, IEnumerable<Post> posts)
        {
            var searchService = new SearchServiceClient(azureSearchName, new SearchCredentials(azureSearchKey));

            if (!await searchService.Indexes.ExistsAsync(this.IndexName))
            {
                var fields = new Field[]
           {
                new Field("id", DataType.String) { IsKey = true },
                new Field("url", DataType.String) { IsSearchable = true },
                new Field("publicationDate", DataType.DateTimeOffset) { IsFilterable = true, IsSortable = true },
                new Field("tags", DataType.Collection(DataType.String)) { IsSearchable = true, IsFilterable = true, IsFacetable = true },
                new Field("category", DataType.String) { IsSearchable = true, IsFilterable = true,  IsSortable = true},
                new Field("title", DataType.String) { IsSearchable = true, IsSortable = true, IsFilterable = true },
                new Field("description", DataType.String) { IsSearchable = true },
                new Field("content", DataType.String) { IsSearchable = true, IsRetrievable = false }
           };

                var index = new Microsoft.Azure.Search.Models.Index
                {
                    Name = this.IndexName,
                    Fields = fields
                };

                await searchService.Indexes.CreateAsync(index);
            }

            var indexClient = searchService.Indexes.GetClient(this.IndexName);

            var batch = IndexBatch.Upload(posts.Select(PostSearchModel.FromPost));
            await indexClient.Documents.IndexAsync(batch);
        }
    }
}
