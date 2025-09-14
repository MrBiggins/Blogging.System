using Blogging.System.Business.Logic.Commands;
using Blogging.System.Business.Logic.Exceptions;
using Blogging.System.Business.Logic.Handlers.Interfaces;
using Blogging.System.Business.Logic.Models;
using Blogging.System.Domain.Entites;
using Blogging.System.Infrastructure.Interfaces;

namespace Blogging.System.Business.Logic.Handlers.Implementations {
    public class CreatePostHandler : ICreatePostHandler {

        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;
        public CreatePostHandler(IPostRepository postRepository, IAuthorRepository authorRepository) {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
        }
        public async Task<PostModel> HandlePostCreation(CreatePostCommand command) {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var authorDto = await _authorRepository.GetAuthorById(command.AuthorId);
            if (authorDto == null) { throw new AuthorNotFoundException($"Author with: {command.AuthorId} not found"); }

            var postDto = new PostEntity(command.AuthorId, command.Title, command.Description, command.Content);
            var calculatedId = await _postRepository.AddPost(postDto);

            return new PostModel
            {
                Author = new AuthorModel { Id = authorDto.Id, Name = authorDto.Name, Surname = authorDto.Surname },
                Id = calculatedId,
                AuthorId = command.AuthorId,
                Title = command.Title,
                Description = command.Description,  
                Content = command.Content
            };
        }
    }
}
