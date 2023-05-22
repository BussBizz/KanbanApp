namespace KanbanApp.Models
{
    public class KanbanTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }

        // FK
        public int CategoryId { get; set; }
        public int? CreatorId { get; set; }
        public int? AssignedId { get; set; }
        public int? CompletedById { get; set; }
        public bool TaskCompleted { get; set; }

        // Navigation prop
        public Category? Category { get; set; }
        public Member? Creator { get; set; }
        public Member? Assigned { get; set; }
        public Member? CompletedBy { get; set; }

        // Collection prop
        public List<Comment> Comments { get; set; } = null!;
        public KanbanTask()
        {
            TaskCompleted = false;
            Comments = new List<Comment>();
        }
    }
}
