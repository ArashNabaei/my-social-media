using Dapper;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly DapperContext _dapperContext;

        public ChatRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task SendMessage(int senderId, int receiverId, string message)
        {
            var createdAt = DateTime.UtcNow;

            var parameters = new DynamicParameters();
            parameters.Add("senderId", senderId);
            parameters.Add("receiverId", receiverId);
            parameters.Add("message", message);
            parameters.Add("createdAt", createdAt);

            var query = "INSERT INTO Messages (SenderId, ReceiverId, Content, CreatedAt, IsDeleted) " +
                "VALUES (@senderId, @receiverid, @message, @createdAt, 0)";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

    }
}
