﻿
namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Caption { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public int IsDeleted { get; set; }

    }
}
