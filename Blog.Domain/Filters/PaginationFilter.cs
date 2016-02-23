using System.Linq;

namespace Blog.Domain.Filters
{
    public class PaginationFilter<T>
    {
        IQueryable<T> _queryable;

        int? _skip;

        int? _take;

        public PaginationFilter(IQueryable<T> queryable, int? skip, int? take)
        {
            _queryable = queryable;
            _skip = skip;
            _take = take;
        }

        public IQueryable<T> Build()
        {
            var query = _queryable;
            if (_skip.HasValue)
            {
                query = query.Skip(_skip.Value);
            }

            if (_take.HasValue)
            {
                query = query.Take(_take.Value);
            }

            return query;
        }
    }

    public static class PaginationFilterExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int? skip, int? take)
        {
            var filter = new PaginationFilter<T>(query, skip, take);

            return filter.Build();
        }
    }
}
