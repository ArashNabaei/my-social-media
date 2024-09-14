using Domain.Entities;

namespace Test.Mocks
{
    public static class FollowMocks
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
            };
        }

    }
}
