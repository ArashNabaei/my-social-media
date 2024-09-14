using Application.Dtos;

namespace Test.Mocks
{
    public static class PostMocks
    {

        public static PostDto ValidPost()
        {
            return new PostDto
            {
                Id = 1,
                ImageUrl = "imageUrl", 
                Caption = "caption", 
                CreatedAt = DateTime.UtcNow,
            };
        }

    }
}
