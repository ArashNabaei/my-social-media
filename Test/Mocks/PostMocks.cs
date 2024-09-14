using Application.Dtos;
using Domain.Entities;

namespace Test.Mocks
{
    public static class PostMocks
    {

        public static Post ValidPost()
        {
            return new Post
            {
                Id = 1,
                ImageUrl = "imageUrl", 
                Caption = "caption", 
                CreatedAt = DateTime.UtcNow,
            };
        }

    }
}
