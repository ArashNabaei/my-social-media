using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPosts();

        Task<Post> GetPostById(int id);

        Task CreatePost(Post post);

        Task DeletePost(int id);

        Task UpdatePost(int id, Post post);

    }
}
