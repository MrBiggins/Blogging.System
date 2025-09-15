using Blogging.System.Domain.Entites;
using Blogging.System.Infrastructure.Interfaces;

namespace Blogging.System.Infrastructure.Repositories {
    public class AuthorRepository : IAuthorRepository {


        private readonly BlogSystemDbContext _blogSystemDbContext;
        public AuthorRepository(BlogSystemDbContext blogSystemDbContext) {
            _blogSystemDbContext = blogSystemDbContext;
        }

        public async Task<AuthorEntity> GetAuthorById(int id) {
            return await _blogSystemDbContext.Authors.FindAsync(id);
        }
    }
}
