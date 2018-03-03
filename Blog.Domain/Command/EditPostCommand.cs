using Blog.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Command
{
    public class EditPostCommand
    {
        private readonly IBlogContext context;

        public EditPostCommand(IBlogContext context)
        {
            this.context = context;
        }

        public async Task<int> ExecuteAsync(Post post)
        {
            this.context.Posts.Attach(post);
            this.context.Entry(post).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await this.context.SaveChangesAsync();
        }
    }
}
