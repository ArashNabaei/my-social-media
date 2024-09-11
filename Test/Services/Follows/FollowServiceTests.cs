using Application.Services.Follows;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.Services.Follows
{
    public class FollowServiceTests
    {

        private readonly Mock<IFollowRepository> _followRepository;

        private readonly Mock<ILogger<FollowService>> _logger;

        private readonly IFollowService _followService;

        public FollowServiceTests()
        {
            _followRepository = new Mock<IFollowRepository>();

            _logger = new Mock<ILogger<FollowService>>();

            _followService = new FollowService(
                _followRepository.Object,
                _logger.Object
                );
        }

    }
}
