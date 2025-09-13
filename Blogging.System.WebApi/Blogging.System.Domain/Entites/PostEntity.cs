namespace Blogging.System.Domain.Entites {
    public record PostEntity {
        public int Id { get; private set; }
        public int AuthorId { get; init; }
        public string Title { get; init; }
        public string Description { get; init; } 
        public string Content { get; init; } 

        public PostEntity(int authorId, string title, string description, string content) {
            AuthorId = authorId;
            Title = title;
            Description = description;
            Content = content;
        }
    }
}
