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

            var query = "SELECT * FROM Posts " +
                "LEFT JOIN Users " +
                "ON Posts.UserId = Users.Id " +
                "WHERE UserId = @userId";

            var posts = await _dapperContext.Connection.QueryAsync<Post>(query, parameters);

            return posts;
        }

        public async Task<Post> GetPostById(int userId, int postId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("postId", postId);

            var query = "SELECT * FROM Posts " +
                "LEFT JOIN Users " +
                "ON Posts.UserId = Users.Id " +
                "WHERE UserId = @userId AND Id = postId";

            var post = await _dapperContext.Connection.QueryFirstOrDefaultAsync<Post>(query, parameters);

            return post;
        }

    }
}
