using Blog.Domain.Entities;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.Threading.Tasks;

namespace Blog.Domain.Queries
{
    public class GetPostsFromSearchQuery
    {
        private readonly SearchServiceClient searchServiceClient;

        public GetPostsFromSearchQuery(SearchServiceClient searchServiceClient)
        {
            this.searchServiceClient = searchServiceClient;
        }

        public async Task<DocumentSearchResult<PostSearchModel>> ExecuteAsync(string term, SearchParameters parameters = null, SearchRequestOptions options = null)
        {
            var searchIndexClient = this.searchServiceClient.Indexes.GetClient("posts");

            return await searchIndexClient.Documents.SearchAsync<PostSearchModel>(term, parameters, options);
        }
    }
}
