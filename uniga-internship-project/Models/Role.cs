namespace uniga_internship_project.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
