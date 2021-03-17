using Microsoft.EntityFrameworkCore;

namespace LecturerService
{
    public class LSContext : DbContext
    {
        public DbSet<Models.Lecturer> Lecturers { get; set; }

        public LSContext(DbContextOptions<LSContext> options): base(options)
        {
            // Creates the database !! Just for DEMO !! in production code you have to handle it differently!  
            Database.EnsureCreated();  
        }
    }
}