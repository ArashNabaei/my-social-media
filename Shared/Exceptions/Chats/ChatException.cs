namespace Shared.Exceptions.Chats
{
    public class ChatException : BaseException
    {

        public ChatException(int code, string message) : base(code, message)
        {
            
        }

        public static ChatException MessageNotFound()
        {
            return new ChatException(4001, "Message was not found.");
        }

        public static ChatException NoMessagesFound()
        {
            return new ChatException(4002, "No messages found in this chat.");
        }

        public static ChatException UserNotFound()
        {
            return new ChatException(4003, "No user was found.");
        }

    }
}
