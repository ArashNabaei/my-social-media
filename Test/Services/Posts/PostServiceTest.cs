using Application.Services.Posts;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Services.Posts
{
    public class PostServiceTest
    {

        private readonly Mock<IPostRepository> _postRepository;

        private readonly Mock<ILogger<PostService>> _logger;

        private readonly PostService _postService;

        public PostServiceTest()
        {
            _postRepository = new Mock<IPostRepository>();

            _logger = new Mock<ILogger<PostService>>();

            _postService = new PostService(
                _postRepository.Object, 
                _logger.Object
                );
        }

    }
}
