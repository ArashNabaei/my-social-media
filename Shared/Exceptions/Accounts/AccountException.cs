
namespace Shared.Exceptions.Accounts
{
    public class AccountException : BaseException
    {
        public AccountException(int code, string message) : base(code, message)
        {
        }

        public static AccountException UserAlreadyExists()
        {
            return new AccountException(1001, "User already exists.");
        }

        public static AccountException UserNotFound()
        {
            return new AccountException(1002, "User was not found.");
        }

    }
}
