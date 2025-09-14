using Blogging.System.Business.Logic.Handlers.Implementations;
using Blogging.System.Business.Logic.Queries;
using Blogging.System.Domain.Entites;
using Blogging.System.Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.System.Test.HandlersTests {
    [TestClass]
    public class BloggingSystemGetHandlerTest {

        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IAuthorRepository> _authorRepositoryMock;
        private GetPostHandler _handler;

        [TestInitialize]
        public void Setup() {
            _postRepositoryMock = new Mock<IPostRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _handler = new GetPostHandler(_postRepositoryMock.Object, _authorRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetPostHandlerReturnCorrectResulOnGetWithoutAuthor() {

            // Arrange
            var postId = 1;
            var authorId = 2;
            var postDto = new PostEntity(authorId, "Test Title", "Test Description", "Test Content");
            var query = new GetPostQuery { Id = postId, IncludeAuthor = false };

            _postRepositoryMock.Setup(repo => repo.GetPostById(postId, query.IncludeAuthor))
                .ReturnsAsync(postDto);

            // Act
            var result = await _handler.HandleGetPostById(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(authorId, result.AuthorId);
            Assert.AreEqual("Test Title", result.Title);
            Assert.AreEqual("Test Description", result.Description);
            Assert.AreEqual("Test Content", result.Content);
            Assert.IsNull(result.Author);

        }

        [TestMethod]
        public async Task GetPostHandlerReturnCorrectResulOnGetWithAuthor() {

            // Arrange
            var postId = 1;
            var authorId = 2;
            var authorDto = new AuthorEntity("John", "Doe");
            var postDto = new PostEntity(authorId, "Test Title", "Test Description", "Test Content", authorDto);
         

            var query = new GetPostQuery { Id = postId, IncludeAuthor = true };

            _postRepositoryMock.Setup(repo => repo.GetPostById(postId, query.IncludeAuthor))
                .ReturnsAsync(postDto);


            // Act
            var result = await _handler.HandleGetPostById(query);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Author);
            Assert.AreEqual(authorId, result.AuthorId);
            Assert.AreEqual("John", result.Author.Name);
            Assert.AreEqual("Doe", result.Author.Surname);
            Assert.AreEqual("Test Title", result.Title);
            Assert.AreEqual("Test Description", result.Description);
            Assert.AreEqual("Test Content", result.Content);

        }

        [TestMethod]
        public async Task GetPostHandlerReturnNullIfPostNotFound() {
            // Arrange
            var postId = 1;

            var query = new GetPostQuery { Id = postId, IncludeAuthor = false };
            _postRepositoryMock.Setup(repo => repo.GetPostById(postId, query.IncludeAuthor))
                .ReturnsAsync((PostEntity)null);
            // Act
            var result = await _handler.HandleGetPostById(query);

            // Assert
            Assert.IsNull(result);
        }
    }
}
