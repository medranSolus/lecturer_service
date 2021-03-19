using Microsoft.EntityFrameworkCore;

namespace LecturerService.Data
{
    public class LSContext : DbContext
    {
        public DbSet<Lecturer> Lecturers { get; set; }

        public LSContext(DbContextOptions<LSContext> options): base(options) {}
    }
}