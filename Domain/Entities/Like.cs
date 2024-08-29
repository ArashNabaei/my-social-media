namespace Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
