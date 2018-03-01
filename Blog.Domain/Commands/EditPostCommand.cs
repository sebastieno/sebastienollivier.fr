using Blog.Data;
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

        public Task<int> ExecuteAsync(Post post)
        {
            this.context.Posts.Attach(post);
            return this.context.SaveChangesAsync();
        }
    }
}