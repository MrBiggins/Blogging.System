using Blogging.System.Business.Logic.Commands;
using Blogging.System.Business.Logic.Exceptions;
using Blogging.System.Business.Logic.Handlers.Implementations;
using Blogging.System.Domain.Entites;
using Blogging.System.Infrastructure.Interfaces;
using Moq;

namespace Blogging.System.Test.HandlersTests {
    [TestClass]
    public class BloggingSystemPostHandlerTest {

        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IAuthorRepository> _authorRepositoryMock;
        private CreatePostHandler _handler;

        [TestInitialize]
        public void Setup() {
            _postRepositoryMock = new Mock<IPostRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _handler = new CreatePostHandler(_postRepositoryMock.Object, _authorRepositoryMock.Object);
        }

        [TestMethod]
        public async Task PostHandlerReturnCorrectResulOnCreate() {

            //Arrange
            var command = new CreatePostCommand
            {
                AuthorId = 1,
                Title = "Test Title",
                Description = "Test Description",
                Content = "Test Content"
            };


            var author = new AuthorEntity("John", "Doe");
            _authorRepositoryMock.Setup(repo => repo.GetAuthorById(1))
                .ReturnsAsync(author);

            _postRepositoryMock.Setup(repo => repo.AddPost(It.IsAny<PostEntity>()))
                .ReturnsAsync(10);

            //Act

            var result = await _handler.HandlePostCreation(command);


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Id);
            Assert.AreEqual("Test Title", result.Title);
            Assert.AreEqual("John", result.Author.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthorNotFoundException))]
        public async Task PostHandlerThrowAuthorNotFoundExceptionCreate() {

            //Arrange
            var command = new CreatePostCommand { AuthorId = 777 };

            _authorRepositoryMock.Setup(repo => repo.GetAuthorById(777))
                .ReturnsAsync((AuthorEntity)null);

            //Act

            var result = await _handler.HandlePostCreation(command);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PostHandlerArgumentNullExceptionCreateWithNullCommand() {
            // Act
            await _handler.HandlePostCreation(null);

        }
    }
}
