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

    }
}
