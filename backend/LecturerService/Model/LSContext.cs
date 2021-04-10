using Microsoft.EntityFrameworkCore;

namespace LecturerService.Model
{
    public class LSContext : DbContext
    {
        public DbSet<CourseMsg> CoursesToCheck { get; set; }
        public DbSet<GroupMsg> GroupNotification { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public LSContext(DbContextOptions<LSContext> options): base(options) {}
    }
}