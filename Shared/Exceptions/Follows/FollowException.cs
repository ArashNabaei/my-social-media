namespace Shared.Exceptions.Follows
{
    public class FollowException : BaseException
    {
        public FollowException(int code, string message) : base(code, message)
        {
        }

        public static FollowException FollowerNotFound()
        {
            return new FollowException(4001, "Follower was not found.");
        }

        public static FollowException FollowingNotFound()
        {
            return new FollowException(4002, "Following was not found.");
        }

    }
}
