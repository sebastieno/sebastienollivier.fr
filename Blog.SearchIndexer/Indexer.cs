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
        public async Task LaunchIndexation(string databaseConnectionString, string azureSearchName, string azureSearchKey)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
            optionsBuilder.UseSqlServer(databaseConnectionString);

            using (var dbContext = new BlogContext(optionsBuilder.Options))
            {
                var posts = await new GetPostsQuery(dbContext).Build().ToListAsync();
            }
        }

        public async Task IndexPosts(string azureSearchName, string azureSearchKey, IEnumerable<Post> posts)
        {
            var searchService = new SearchServiceClient(azureSearchName, new SearchCredentials(azureSearchKey));

            var fields = new Field[]
            {
                new Field("id", DataType.String) { IsKey = true },
                new Field("url", DataType.String) { IsSearchable = true },
                new Field("publicationDate", DataType.DateTimeOffset) { IsFilterable = true, IsSortable = true },
                new Field("tags", DataType.Collection(DataType.String)) { IsSearchable = true, IsFilterable = true, IsFacetable = true },
                new Field("category", DataType.String) { IsSearchable = true, IsFilterable = true,  IsSortable = true},
                new Field("title", DataType.String) { IsSearchable = true, IsSortable = true, IsFilterable = true },
                new Field("description", DataType.String) { IsSearchable = true },
                new Field("content", DataType.String) { IsSearchable = true }
            };

            var index = new Microsoft.Azure.Search.Models.Index
            {
                Name = "posts",
                Fields = fields
            };

            if (!await searchService.Indexes.ExistsAsync(index.Name))
            {
                await searchService.Indexes.CreateAsync(index);
            }

            var indexClient = searchService.Indexes.GetClient("post");

            var batch = IndexBatch.Upload(posts.Select(PostSearchModel.FromPost));
            await indexClient.Documents.IndexAsync(batch);
        }
    }
}
