using Microsoft.EntityFrameworkCore;
using Project_GradeBook_Web.Models;

namespace Project_GradeBook_Web.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
