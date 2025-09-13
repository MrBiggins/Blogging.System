using Blogging.System.Business.Logic.Handlers.Implementations;
using Blogging.System.Business.Logic.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Blogging.System.Business.Logic {
    public static class DependecyInjection {
        public static void AddBusinessLogic(this IServiceCollection serviceCollection) {
            serviceCollection.AddScoped<ICreatePostHandler, CreatePostHandler>();
            serviceCollection.AddScoped<IGetPostHandler, GetPostHandler>();
        }
    }
}
