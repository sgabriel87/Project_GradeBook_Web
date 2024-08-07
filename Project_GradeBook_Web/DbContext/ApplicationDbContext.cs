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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-one relationship between Student and Address
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Address)       // Student has one Address
                .WithOne(a => a.Student)      // Address has one Student
                .HasForeignKey<Student>(s => s.AddressId) // Foreign key in Student
                .OnDelete(DeleteBehavior.Cascade); // Optional: Handle cascading delete if needed
        }
    }

}
