
namespace Shared.Exceptions.Posts
{
    public class PostException : BaseException
    {

        public PostException(int code, string message) : base(code, message)
        {
            
        }

        public static PostException PostNotFound()
        {
            return new PostException(3001, "Post was not found.");
        }

        public static PostException PostAlreadyExists()
        {
            return new PostException(3002, "Post already exists.");
        }

    }
}
