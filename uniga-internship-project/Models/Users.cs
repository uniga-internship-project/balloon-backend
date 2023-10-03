﻿namespace uniga_internship_project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
