using Microsoft.EntityFrameworkCore;
using System.Numerics;
using uniga_internship_project.Models;

namespace uniga_internship_project.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> User => Set<User>();
        public DbSet<Role> Role => Set<Role>();
        public DbSet<Skill> Skill => Set<Skill>();
        public DbSet<Position> Position => Set<Position>();
        public DbSet<Plan> Plan => Set<Plan>();
    }
}
