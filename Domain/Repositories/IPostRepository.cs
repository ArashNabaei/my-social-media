using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts(int userId);

        Task<Post> GetPostById(int userId, int postId);

        Task CreatePost(int userId, Post post);

        Task DeletePost(int userId, int postId);

        Task UpdatePost(int userId, int postId, Post post);

        Task LikePost(int userId, int postId);

        Task<IEnumerable<Like>> GetLikesOfPost(int userId, int postId);

        Task<Post> GetOthersPostById(int userId, int postId);

        Task<IEnumerable<Post>> GetFriendsPosts(int userId, int friendId);

        Task LeaveCommentOnPost(int userId, int postId, string comment);
    }
}
