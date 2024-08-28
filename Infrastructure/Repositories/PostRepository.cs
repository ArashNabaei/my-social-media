using Dapper;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly DapperContext _dapperContext;

        public PostRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }


        public async Task<IEnumerable<Post>> GetAllPosts(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);

            var query = @"SELECT 
                    Posts.Id,
                    Posts.ImageUrl,
                    Posts.Caption,
                    Posts.CreationTime
                  FROM Posts
                  INNER JOIN Users ON Posts.UserId = Users.Id
                  WHERE Posts.UserId = @userId AND IsDeleted = 0";

            var posts = await _dapperContext.Connection.QueryAsync<Post>(query, parameters);

            return posts;
        }


        public async Task<Post> GetPostById(int userId, int postId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);

            var query = @"SELECT 
                    Posts.Id,
                    Posts.ImageUrl,
                    Posts.Caption,
                    Posts.CreationTime
                  FROM Posts
                  INNER JOIN Users ON Posts.UserId = Users.Id
                  WHERE Posts.UserId = @userId AND Posts.Id = @postId AND IsDeleted = 0";

            var post = await _dapperContext.Connection.QueryFirstOrDefaultAsync<Post>(query, parameters);

            return post;
        }

        public async Task CreatePost(int userId,  Post post)
        {
            var parameters = new DynamicParameters();
            parameters.Add("caption", post.Caption);
            parameters.Add("imageUrl", post.ImageUrl);
            parameters.Add("creationTime", post.CreationTime);
            parameters.Add("userId", userId);

            var query = "INSERT INTO Posts (Caption, ImageUrl, CreationTime, UserId, IsDeleted) " +
                "VALUES (@caption, @imageUrl, @creationTime, @userId, 0)";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);

        }

        public async Task DeletePost(int userId, int postId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);

            var query = "UPDATE Posts " +
                "SET IsDeleted = 1 " +
                "WHERE Id = @postId AND UserId = @userId";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task UpdatePost(int userId, int postId, Post post)
        {
            var parameters = new DynamicParameters();
            parameters.Add("postId", postId);
            parameters.Add("caption", post.Caption);
            parameters.Add("imageUrl", post.ImageUrl);
            parameters.Add("userId", userId);

            var query = "UPDATE Posts " +
                "SET Caption = @caption, ImageUrl = @imageUrl " +
                "WHERE Id = @postId AND UserId = @userId";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task LikePost(int userId, int postId)
        {
            var createdAt = DateTime.UtcNow;

            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);
            parameters.Add("createdAt", createdAt);

            var query = "INSERT INTO Likes (UserId, PostId, CreatedAt) " +
                "VALUES (@userId, @postId, @createdAt)";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

    }
}
