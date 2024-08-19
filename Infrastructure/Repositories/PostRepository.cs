
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
            var query = "SELECT * FROM Posts LEFT JOIN Users ON Posts.UserId = Users.Id";

            var posts = await _dapperContext.Connection.QueryAsync<Post>(query);

            return posts;
        }

    }
}
