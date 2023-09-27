using Microsoft.EntityFrameworkCore;
using uniga_internship_project.Models;

namespace uniga_internship_project.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Users> Users => Set<Users>();
    }
}
