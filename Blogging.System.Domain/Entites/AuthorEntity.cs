namespace Blogging.System.Domain.Entites {
    public record AuthorEntity {
        public int Id { get; private set; }         
        public string Name { get; init; }
        public string Surname { get; init; }

        public virtual ICollection<PostEntity> Posts { get; private set; } = new List<PostEntity>();
        
        public AuthorEntity() { }
        
        public AuthorEntity(string name, string surname) {
            Name = name;
            Surname = surname;
        }
    }
}
