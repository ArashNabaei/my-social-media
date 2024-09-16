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


        public async Task<IEnumerable<Post>?> GetAllPosts(int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);

            var query = @"SELECT 
                    Posts.Id,
                    Posts.ImageUrl,
                    Posts.Caption,
                    Posts.CreatedAt
                  FROM Posts
                  INNER JOIN Users ON Posts.UserId = Users.Id
                  WHERE Posts.UserId = @userId AND IsDeleted = 0";

            var posts = await _dapperContext.Connection.QueryAsync<Post>(query, parameters);

            return posts;
        }


        public async Task<Post?> GetPostById(int userId, int postId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);

            var query = @"SELECT 
                    Posts.Id,
                    Posts.ImageUrl,
                    Posts.Caption,
                    Posts.CreatedAt
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
            parameters.Add("createdAt", post.CreatedAt);
            parameters.Add("userId", userId);

            var query = "INSERT INTO Posts (Caption, ImageUrl, CreatedAt, UserId, IsDeleted) " +
                "VALUES (@caption, @imageUrl, @createdAt, @userId, 0)";

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

        public async Task<IEnumerable<Like>?> GetLikesOfPost(int userId, int postId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);

            var query = "SELECT l.Id, " +
                "l.UserId, " +
                "u.Username, " +
                "l.CreatedAt " +
                "FROM Likes l " +
                "INNER JOIN Users u " +
                "ON u.Id = l.UserId " +
                "INNER JOIN Posts p " +
                "ON p.Id = l.PostId " +
                "WHERE p.Id = @postId AND p.UserId = @userId";

            var likes = await _dapperContext.Connection.QueryAsync<Like>(query, parameters);

            return likes;
        }

        public async Task<Post?> GetOthersPostById(int userId, int postId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);

            var query = "SELECT p.Id, " +
                "p.UserId, " +
                "p.Caption, " +
                "p.ImageUrl, " +
                "p.CreatedAt " +
                "FROM Posts p " +
                "INNER JOIN Follows f1 " +
                "ON f1.FollowerId = @userId AND f1.FollowingId = p.UserId " +
                "INNER JOIN Follows f2 " +
                "ON f2.FollowerId = p.UserId AND f2.FollowingId = @userId " +
                "WHERE p.Id = @postId";

            var post = await _dapperContext.Connection.QueryFirstOrDefaultAsync<Post>(query, parameters);

            return post;
        }

        public async Task<IEnumerable<Post>?> GetFriendsPosts(int userId, int friendId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("friendId", friendId);

            var query = "SELECT p.Id, " +
                "p.UserId, " +
                "p.Caption, " +
                "p.ImageUrl, " +
                "p.CreatedAt " +
                "FROM Posts p " +
                "INNER JOIN Follows f1 " +
                "ON f1.FollowerId = @userId AND f1.FollowingId = p.UserId " +
                "INNER JOIN Follows f2 " +
                "ON f2.FollowerId = p.UserId AND f2.FollowingId = @userId " +
                "WHERE p.UserId = @friendId";

            var posts = await _dapperContext.Connection.QueryAsync<Post>(query, parameters);

            return posts;
        }

        public async Task LeaveCommentOnPost(int userId, int postId, string comment)
        {
            var createdAt = DateTime.UtcNow;

            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);
            parameters.Add("comment", comment);
            parameters.Add("createdAt", createdAt);

            var query = "INSERT INTO Comments (UserId, PostId, Content, CreatedAt) " +
                "VALUES (@userId, @postId, @comment, @createdAt)";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<Comment>?> GetCommentsOfPost(int userId, int postId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);

            var query = "SELECT c.Id, " +
                "c.UserId, " +
                "u.Username, " +
                "c.CreatedAt " +
                "FROM Comments c " +
                "INNER JOIN Users u " +
                "ON u.Id = c.UserId " +
                "INNER JOIN Posts p " +
                "ON p.Id = c.PostId " +
                "WHERE p.Id = @postId AND c.UserId = @userId";

            var comments = await _dapperContext.Connection.QueryAsync<Comment>(query, parameters);

            return comments;
        }

        public async Task ReportPost(int userId, int postId, string message)
        {
            var createdAt = DateTime.UtcNow;

            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);
            parameters.Add("message", message);
            parameters.Add("createdAt", createdAt);

            var query = "INSERT INTO Reports (Id, PostId, UserId, Message, CreatedAt) " +
                "VALUES (@postId, @userId, @message, @createdAt)";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

    }
}
