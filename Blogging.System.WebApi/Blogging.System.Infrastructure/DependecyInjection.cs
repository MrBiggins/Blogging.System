using Blogging.System.Infrastructure.Interfaces;
using Blogging.System.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blogging.System.Infrastructure {
    public static class DependecyInjection {
        public static void AddInfrastructure(this IServiceCollection serviceCollection) {
            serviceCollection.AddDbContext<BlogSystemDbContext>(o =>
                 o.UseInMemoryDatabase("BlogSystemDb"));

            serviceCollection.AddScoped<IAuthorRepository, AuthorRepository>();
            serviceCollection.AddScoped<IPostRepository, PostRepository>();
        }
    }
}
