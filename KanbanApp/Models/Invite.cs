using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Models
{
    public class Invite
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public DateTime? Expire { get; set; }

        // FK
        public int? UserId { get; set; }
        public int BoardId { get; set; }

        // Navigation prop
        public User? User { get; set; }
        public Board Board { get; set; } = null!;
    }
}
