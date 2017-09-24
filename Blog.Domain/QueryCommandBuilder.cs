using System;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Domain
{
    public class QueryCommandBuilder
    {
        private IServiceProvider provider;

        public QueryCommandBuilder(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public T Build<T>()
        {
            return this.provider.GetRequiredService<T>();
        }
    }
}