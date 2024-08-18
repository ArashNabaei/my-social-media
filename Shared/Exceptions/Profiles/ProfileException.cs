
namespace Shared.Exceptions.Profiles
{
    public class ProfileException : BaseException
    {

        public ProfileException(int code, string message) : base(code, message)
        {
            
        }

        public static ProfileException ProfileNotFound()
        {
            return new ProfileException(2001, "Profile was not found.");
        }

        public static ProfileException InvalidProfileData()
        {
            return new ProfileException(2002, "Invalid data.");
        }

    }
}
