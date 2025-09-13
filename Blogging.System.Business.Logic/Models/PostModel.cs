namespace Blogging.System.Business.Logic.Models {
    public class PostModel {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }

    }
}
