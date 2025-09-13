using Blogging.System.Business.Logic.Handlers.Interfaces;
using Blogging.System.Business.Logic.Models;
using Blogging.System.Business.Logic.Queries;
using Blogging.System.Infrastructure.Interfaces;

namespace Blogging.System.Business.Logic.Handlers.Implementations {
    public class GetPostHandler : IGetPostHandler {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;

        public GetPostHandler(IPostRepository postRepository, IAuthorRepository authorRepository) {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
        }
        public async Task<PostModel> HandleGetPostById(GetPostQuery query) {

            var postDto = await _postRepository.GetPostById(query.Id);
            if (postDto == null) { return null; }

            var postModel = new PostModel {
                Id = postDto.Id,
                AuthorId = postDto.AuthorId,
                Title = postDto.Title,
                Description = postDto.Description,
                Content = postDto.Content
            };

            if (query.IncludeAuthor) {
                var authorDto = await _authorRepository.GetAuthorById(postDto.AuthorId);
                if (authorDto != null) {
                    postModel.Author = new AuthorModel
                    {
                        Id = authorDto.Id,
                        Name = authorDto.Name,
                        Surname = authorDto.Surname
                    };
                }
            }

            return postModel;
        }
    }
}
