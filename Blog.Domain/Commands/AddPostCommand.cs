using Blog.Data;
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

        public Task<int> ExecuteAsync(Post post)
        {
            this.context.Posts.Add(post);
            return context.SaveChangesAsync();
        }
    }
}