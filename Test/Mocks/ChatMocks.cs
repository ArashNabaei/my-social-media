using Domain.Entities;

namespace Test.Mocks
{
    public static class ChatMocks
    {

        public static Message ValidMessage()
        {
            return new Message
            {
                Id = 1, SenderId = 1, ReceiverId = 2, Content = "valid message"
            };
        }

    }
}
