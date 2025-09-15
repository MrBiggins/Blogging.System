namespace Blogging.System.Business.Logic.Exceptions {
    public class AuthorNotFoundException : Exception {
        public AuthorNotFoundException(string message) : base(message) { }
    }
}
