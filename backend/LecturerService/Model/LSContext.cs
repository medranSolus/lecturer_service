using Microsoft.EntityFrameworkCore;

namespace LecturerService.Model
{
    public class LSContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public LSContext(DbContextOptions<LSContext> options): base(options) {}
    }
}