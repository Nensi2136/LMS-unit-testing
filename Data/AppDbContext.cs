using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectWiseFaculty> SubjectWiseFaculties { get; set; }
        public DbSet<Material> Materials { get; set; }
    }
}
