namespace uniga_internship_project.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
