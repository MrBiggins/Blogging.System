namespace Blogging.System.Domain.Entites {
    public record AuthorEntity {
        public int Id { get; private set; }         
        public string Name { get; init; }
        public string Surname { get; init; }

        public AuthorEntity(string name, string surname) {
            Name = name;
            Surname = surname;
        }
    }
}
