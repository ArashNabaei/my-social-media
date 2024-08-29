using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Posts
{
    public interface IPostService
    {

        Task<IEnumerable<PostDto>> GetAllPosts(int userId);

        Task<PostDto> GetPostById(int userId, int postId);

        Task CreatePost(int userId, PostDto post);

        Task DeletePost(int userId, int postId);

        Task UpdatePost(int userId, int postId, PostDto post);

        Task LikePost(int userId, int postId);

        Task<IEnumerable<Like>> GetLikesOfPost(int userId, int postId);

        Task<IEnumerable<Post>> GetFriendsPosts(int userId, int friendId);
    }
}
