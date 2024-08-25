using Application.Dtos;
using Domain.Repositories;

namespace Application.Services.Follows
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllFriends(int userId) 
        {
            var friends = await _followRepository.GetAllFriends(userId);

            var result = friends.Select(friend => new UserDto
            {
                Id = friend.Id,
                FirstName = friend.FirstName,
                LastName = friend.LastName,
                Bio = friend.Bio,
                ImageUrl = friend.ImageUrl,
            });

            return result;
        }

    }
}
