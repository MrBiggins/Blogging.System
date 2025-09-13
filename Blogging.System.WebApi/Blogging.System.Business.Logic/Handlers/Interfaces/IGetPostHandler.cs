using Blogging.System.Business.Logic.Models;
using Blogging.System.Business.Logic.Queries;

namespace Blogging.System.Business.Logic.Handlers.Interfaces {
    public interface IGetPostHandler {
        Task<PostModel> HandleGetPostById(GetPostQuery query);
    }
}
