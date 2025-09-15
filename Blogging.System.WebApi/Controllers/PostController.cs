using Blogging.System.Business.Logic.Commands;
using Blogging.System.Business.Logic.Handlers.Interfaces;
using Blogging.System.Business.Logic.Models;
using Blogging.System.Business.Logic.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blogging.System.WebApi.Controllers {
    [Route("post")]
    [ApiController]
    public class PostController : ControllerBase {
        private readonly IGetPostHandler _getPostHandler;
        private readonly ILogger<PostController> _logger;
        private readonly ICreatePostHandler _createPostHandler;
        public PostController(ICreatePostHandler createPostHandler, IGetPostHandler getPostHandler, ILogger<PostController> logger) {
            _logger = logger;
            _getPostHandler = getPostHandler;
            _createPostHandler = createPostHandler;
        }

        [HttpPost]
        [Consumes("application/json", "application/xml")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(PostModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command) {
            
            var post = await _createPostHandler.HandlePostCreation(command);
            if (post == null) {
                _logger.LogWarning("Post creation returns null");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpGet("{id:int}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(PostModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPost(int id, [FromQuery] bool includeAuthor = false) {
            var post = await _getPostHandler.HandleGetPostById(new GetPostQuery { Id = id, IncludeAuthor = includeAuthor });
            if (post == null) {
                _logger.LogWarning($"Post not found by id {id}");
                return NotFound(new ProblemDetails { Title = "Post not found", Status = 404 });
            }
            return Ok(post);
        }
    }
}
