
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

        public static PostException NoCommentsFound()
        {
            return new PostException(3003, "No comments found for this post.");
        }

        public static PostException NoFriendsPostsFound()
        {
            return new PostException(3004, "No posts found for this friend.");
        }

        public static PostException NoLikesFound()
        {
            return new PostException(3005, "No likes found for this post.");
        }

        public static PostException NoPostsFound()
        {
            return new PostException(3006, "No posts found for this user.");
        }

    }
}
