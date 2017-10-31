using Blog.Domain.Entities;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.Threading.Tasks;

namespace Blog.Domain.Queries
{
    public class GetPostsFromSearchQuery
    {
        private readonly SearchServiceClient searchServiceClient;
        private int? skip;
        private int? take;

        public GetPostsFromSearchQuery(SearchServiceClient searchServiceClient)
        {
            this.searchServiceClient = searchServiceClient;
        }

        public GetPostsFromSearchQuery Paginate(int? skip, int? take)
        {
            this.skip = skip;
            this.take = take;

            return this;
        }

        public async Task<DocumentSearchResult<PostSearchModel>> ExecuteAsync(string term)
        {
            var searchIndexClient = this.searchServiceClient.Indexes.GetClient("posts");

            return await searchIndexClient.Documents.SearchAsync<PostSearchModel>(term, new SearchParameters
            {
                Skip = this.skip,
                Top = this.take,
                IncludeTotalResultCount = true
            });
        }
    }
}
