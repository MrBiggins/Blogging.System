using Blogging.System.Domain.Entites;

namespace Blogging.System.Infrastructure.Interfaces {
    public interface IPostRepository {
        Task<int> AddPost(PostEntity author);
        Task<PostEntity> GetPostById(int id);
    }
}
