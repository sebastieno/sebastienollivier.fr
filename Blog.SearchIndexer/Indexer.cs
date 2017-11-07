using Blog.Data;
using Blog.Domain.Entities;
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
        private readonly string databaseConnectionString;
        private readonly string azureSearchName;
        private readonly string azureSearchKey;
        private readonly string azureSearchIndexName;

        public Indexer(string databaseConnectionString, string azureSearchName, string azureSearchKey, string azureSearchIndexName)
        {
            this.databaseConnectionString = databaseConnectionString;
            this.azureSearchName = azureSearchName;
            this.azureSearchKey = azureSearchKey;
            this.azureSearchIndexName = azureSearchIndexName;
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

            if (!await searchService.Indexes.ExistsAsync(this.azureSearchIndexName))
            {
                var fields = new Field[]
           {
                new Field("id", DataType.String) { IsKey = true },
                new Field("url", DataType.String) { IsSearchable = true },
                new Field("publicationDate", DataType.DateTimeOffset) { IsFilterable = true, IsSortable = true },
                new Field("tags", DataType.Collection(DataType.String)) { IsSearchable = true, IsFilterable = true, IsFacetable = true },
                new Field("category", DataType.String) { IsSearchable = true, IsFilterable = true,  IsSortable = true},
                new Field("categoryCode", DataType.String) { IsSearchable = false, IsFilterable = false},
                new Field("title", DataType.String) { IsSearchable = true, IsSortable = true, IsFilterable = true },
                new Field("description", DataType.String) { IsSearchable = true },
                new Field("content", DataType.String) { IsSearchable = true, IsRetrievable = false }
           };

                var index = new Microsoft.Azure.Search.Models.Index
                {
                    Name = this.azureSearchIndexName,
                    Fields = fields
                };

                await searchService.Indexes.CreateAsync(index);
            }

            var indexClient = searchService.Indexes.GetClient(this.azureSearchIndexName);

            var batch = IndexBatch.Upload(posts.Select(PostSearchModel.FromPost));
            await indexClient.Documents.IndexAsync(batch);
        }
    }
}
