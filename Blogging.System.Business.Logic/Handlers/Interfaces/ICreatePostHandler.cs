using Blogging.System.Business.Logic.Commands;
using Blogging.System.Business.Logic.Models;
using Blogging.System.Domain.Entites;

namespace Blogging.System.Business.Logic.Handlers.Interfaces {
    public interface ICreatePostHandler {
        Task<PostModel> HandlePostCreation(CreatePostCommand command);
    }
}
