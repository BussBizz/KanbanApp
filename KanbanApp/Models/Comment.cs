﻿namespace KanbanApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public byte[]? Image { get; set; }

        // FK
        public int KanbanTaskId { get; set; }
        public int? MemberId { get; set; }

        // Navigation prop
        public KanbanTask? KanbanTask { get; set; }
        public Member? Member { get; set;}
    }
}
