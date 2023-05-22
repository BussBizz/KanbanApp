using System.Collections.ObjectModel;

namespace KanbanApp.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Titel { get; set; } = null!;
        public DateTime? DeadLine { get; set; }

        // Foreign Key
        public int? OwnerId { get; set; }

        // Navigation prop
        public User? Owner { get; set; }

        // Collection prop
        public List<Category> Categories { get; set; } = null!;
        public List<Member> Members { get; set; } = null!;

        public Board() 
        {
            Members = new List<Member>();
            Categories = new List<Category>(); 
        }
    }
}
