using Microsoft.EntityFrameworkCore;
using StudentInfo.Models.Domain;

namespace StudentInfo.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Student> StudentInfos { get; set; }
    }
}
