using Blogging.System.Domain.Entites;
using Blogging.System.Infrastructure.Interfaces;

namespace Blogging.System.Infrastructure.Repositories {
    public class PostRepository : IPostRepository {

        private readonly BlogSystemDbContext _blogSystemDbContext;

        public PostRepository(BlogSystemDbContext blogSystemDbContext) {
            _blogSystemDbContext = blogSystemDbContext;
        }
        public async Task<int> AddPost(PostEntity post) {
            if (post == null) throw new ArgumentNullException(nameof(post));
            _blogSystemDbContext.Posts.Add(post);
            await _blogSystemDbContext.SaveChangesAsync();
            return post.Id;
        }

        public async Task<PostEntity> GetPostById(int id) {
            return await _blogSystemDbContext.Posts.FindAsync(id);
        }
    }
}
