using Blogging.System.Business.Logic.Commands;
using Blogging.System.Business.Logic.Handlers.Interfaces;
using Blogging.System.Business.Logic.Models;
using Blogging.System.Business.Logic.Queries;
using Blogging.System.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.System.Test.ControllerTests {
    [TestClass]
    public class BloggingSystemPostControllerTests {

        private Mock<ICreatePostHandler> _createHandlerMock;
        private Mock<IGetPostHandler> _getHandlerMock;
        private Mock<ILogger<PostController>> _loggerMock;
        private PostController _controller;

        [TestInitialize]
        public void Setup() {
            _createHandlerMock = new Mock<ICreatePostHandler>();
            _getHandlerMock = new Mock<IGetPostHandler>();
            _loggerMock = new Mock<ILogger<PostController>>();

            _controller = new PostController(
                _createHandlerMock.Object,
                _getHandlerMock.Object,
                _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetPostReturnsOkWithExistingId() {
            // Arrange
            var postModel = new PostModel { Id = 1, Title = "Test" };
            _getHandlerMock.Setup(h => h.HandleGetPostById(It.Is<GetPostQuery>(q => q.Id == 1)))
                .ReturnsAsync(postModel);

            // Act
            var result = await _controller.GetPost(1);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedPost = okResult.Value as PostModel;
            Assert.IsNotNull(returnedPost);
            Assert.AreEqual(1, returnedPost.Id);
        }

        [TestMethod]
        public async Task GetPostReturnsNotFoundWithNonExistingId() {
            // Arrange
            _getHandlerMock.Setup(h => h.HandleGetPostById(It.IsAny<GetPostQuery>()))
                .ReturnsAsync((PostModel)null);

            // Act
            var result = await _controller.GetPost(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);

            Assert.IsInstanceOfType(notFoundResult.Value, typeof(ProblemDetails));
            var problemDetails = notFoundResult.Value as ProblemDetails;
            Assert.IsNotNull(problemDetails);

            Assert.AreEqual("Post not found", problemDetails.Title);
            Assert.AreEqual(404, problemDetails.Status);
        }

        [TestMethod]
        public async Task CreatePostReturn500Error() {
            //Arrange
            _createHandlerMock.Setup(h => h.HandlePostCreation(It.IsAny<CreatePostCommand>()))
                .ReturnsAsync((PostModel)null);

            var command = new CreatePostCommand
            {
                AuthorId = 1,
                Title = "Test Title",
                Description = "Test Description",
                Content = "Test Content with more than 10 characters"
            };

            // Act
            var result = await _controller.CreatePost(command);

            // Assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);

        }
    }
}
