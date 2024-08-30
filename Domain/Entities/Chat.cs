namespace Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
