namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
