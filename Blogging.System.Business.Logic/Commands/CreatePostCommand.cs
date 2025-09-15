using System.ComponentModel.DataAnnotations;

namespace Blogging.System.Business.Logic.Commands {
    public record class CreatePostCommand {
        [Range(1, int.MaxValue)]
        public int AuthorId { get; init; }
        [Required, StringLength(200, MinimumLength = 3)]
        public string Title { get; init; } = string.Empty;
        [Required, StringLength(500, MinimumLength = 5)]
        public string Description { get; init; } = string.Empty;
        [Required, MinLength(10)]
        public string Content { get; init; } = string.Empty;
    }
}
