namespace Shared.Exceptions.Follows
{
    public class FollowException : BaseException
    {
        public FollowException(int code, string message) : base(code, message)
        {
        }

        public static FollowException FollowerNotFound()
        {
            return new FollowException(5001, "Follower was not found.");
        }

        public static FollowException FollowingNotFound()
        {
            return new FollowException(5002, "Following was not found.");
        }

        public static FollowException NoFriendsFound()
        {
            return new FollowException(5003, "No friends found.");
        }

        public static FollowException NoFollowersFound()
        {
            return new FollowException(5004, "No followers found.");
        }


    }
}
