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

        public static PostDto UpdatedPost()
        {
            return new PostDto
            {
                Id = 1,
                ImageUrl = "UpdatedImageUrl",
                Caption = "UpdatedCaption",
                CreatedAt = DateTime.UtcNow,
            };
        }

        public static Like ValidLike()
        {
            return new Like
            {
                Id = 1,
                UserId = 1,
                UserName = "username",
                CreatedAt = DateTime.UtcNow,
            };
        }

    }
}
