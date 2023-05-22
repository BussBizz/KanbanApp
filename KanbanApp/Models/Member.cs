namespace KanbanApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public bool CanComplete { get; set; } = true;
        public bool CanCreate { get; set; } = false;
        public bool CanAssign { get; set; } = false;
        public bool CanAdmin { get; set; } = false;
        public bool IsOwner { get; set; } = false;

        // FK
        public int BoardId { get; set; }
        public int UserId { get; set; }

        // Navigation prop
        public Board? Board { get; set; }
        public User? User { get; set; }

        // Collection prop
        public List<KanbanTask> TasksAssigned { get; set;} = null!;
        public List<KanbanTask> TasksCreated { get; set;} = null!;
        public List<KanbanTask> TasksCompleted { get; set; } = null!;
        public List<Comment> Comments { get; set; } = null!;
        public List<Category> Categories { get; set; } = null!;

        public Member()
        {
            TasksAssigned = new List<KanbanTask>();
            TasksCreated = new List<KanbanTask>();
            TasksCompleted = new List<KanbanTask>();
            Comments = new List<Comment>();
            Categories = new List<Category>();
        }
    }
}
