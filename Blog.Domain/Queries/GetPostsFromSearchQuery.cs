using Blog.Domain.Entities;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.Threading.Tasks;

namespace Blog.Domain.Queries
{
    public class GetPostsFromSearchQuery
    {
        private readonly ISearchIndexClient searchIndexClient;
        private int? skip;
        private int? take;

        public GetPostsFromSearchQuery(ISearchIndexClient searchIndexClient)
        {
            this.searchIndexClient = searchIndexClient;
        }

        public GetPostsFromSearchQuery Paginate(int? skip, int? take)
        {
            this.skip = skip;
            this.take = take;

            return this;
        }

        public async Task<DocumentSearchResult<PostSearchModel>> ExecuteAsync(string term)
        {
            return await this.searchIndexClient.Documents.SearchAsync<PostSearchModel>(term, new SearchParameters
            {
                Skip = this.skip,
                Top = this.take,
                IncludeTotalResultCount = true
            });
        }
    }
}
