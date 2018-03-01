using Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Command
{
    public class AddPostCommand
    {
        private readonly IBlogContext context;

        public AddPostCommand(IBlogContext context)
        {
            this.context = context;
        }

        public async Task<int> ExecuteAsync(Post post)
        {
            this.context.Posts.Add(post);
            return await context.SaveChangesAsync();
        }
    }
}
