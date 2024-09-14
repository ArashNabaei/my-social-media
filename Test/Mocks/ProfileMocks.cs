using Domain.Entities;

namespace Test.Mocks
{
    public static class ProfileMocks
    {

        public static User ValidUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "firstname",
                LastName = "lastname",
                Username = "username",
                Password = "password",
                Bio = "bio",
                ImageUrl = "imageUrl",
                Email = "email",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.UtcNow,
            };
        }

        public static User UpdatedUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "UpdatedFirstname",
                LastName = "UpdatedLastname",
                Username = "UpdatedUsername",
                Password = "UpdatedPassword",
                Bio = "UpdatedBio",
                ImageUrl = "UpdatedImageUrl",
                Email = "UpdatedEmail",
                PhoneNumber = "2234567890",
                DateOfBirth = DateTime.UtcNow,
            };
        }

    }
}
