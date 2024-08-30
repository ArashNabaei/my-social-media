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

    }
}
