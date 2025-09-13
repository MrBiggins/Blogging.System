using Blogging.System.Domain.Entites;

namespace Blogging.System.Infrastructure.Interfaces {
    public interface IAuthorRepository {
        Task AddAuthor(AuthorEntity author);
        Task<AuthorEntity> GetAuthorById(int id);
    }
}
