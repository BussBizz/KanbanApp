namespace KanbanApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsAnon { get; set; } = true;

        // Collection prop
        public List<Board> Boards { get; set; } = null!;
        public List<Member> Memberships { get; set; } = null!;

        public User()
        {
            Boards = new List<Board>();
            Memberships = new List<Member>();
        }
    }
}
