using CGPACalculatorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CGPACalculatorApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Student> Students {  get; set; } 
    }
}
