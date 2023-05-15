using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanApp.Models
{
    public class Password
    {
        public int Id { get; set; }
        public string Hash { get; set; } = null!;

        // FK
        public int UserId { get; set; }

        // Navigation Prop
        public User User { get; set; } = null!;

        public Password()
        {
            User = new User();
        }
    }
}
