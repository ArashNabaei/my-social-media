using Dapper;
using Domain.Entities;
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

        public async Task<IEnumerable<Message>> GetAllMessages(int userId, int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("id", id);

            var query = "SELECT m.Id, " +
                "m.SenderId, " +
                "m.ReceiverId, " +
                "m.Content, " +
                "m.CreatedAt " +
                "FROM Messages m " +
                "WHERE (m.SenderId = @userId AND m.ReceiverId = @id AND IsDeleted = 0) " +
                "OR (m.ReceiverId = @userId AND m.SenderId = @id AND IsDeleted = 0)";

            var messages = await _dapperContext.Connection.QueryAsync<Message>(query, parameters);

            return messages;
        }

        public async Task DeleteMessage(int userId, int messageId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("messageId", messageId);

            var query = "UPDATE Messages " +
                "SET IsDeleted = 1 " +
                "WHERE SenderId = @userId AND Id = @messageId";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

        public async Task UpdateMessage(int userId, int messageId, string message)
        {
            var parameters = new DynamicParameters();
            parameters.Add("userId", userId);
            parameters.Add("messageId", messageId);
            parameters.Add("message", message);

            var query = "UPDATE Messages " +
                "SET Content = @message " +
                "WHERE SenderId = @userId AND Id = @messageId AND IsDeleted = 0";

            await _dapperContext.Connection.ExecuteAsync(query, parameters);
        }

    }
}
