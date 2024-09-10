using Domain.Entities;

namespace Test.Mocks
{
    public static class AccountMocks
    {

        public static User ValidUser()
        {
            return new User
            {
                Id = 1,
                Username = "ValidUsername",
                Password = "ValidPassword"
            };
        }

        public static User InvalidUser()
        {
            return new User
            {
                Username = "InvalidUsername",
                Password = "InvalidPassword"
            };
        }

        public static User ExistingUser()
        {
            return new User
            {
                Id = 1,
                Username = "ExistingUsername",
                Password = "ExistingPassword"
            };
        }

    }
}
