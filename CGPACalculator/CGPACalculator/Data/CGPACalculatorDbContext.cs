using CGPACalculator.Models;
using Microsoft.EntityFrameworkCore;

namespace CGPACalculator.Data
{
    public class CGPACalculatorDbContext : DbContext
    {
        public CGPACalculatorDbContext(DbContextOptions options) : base(options) { }
        public DbSet<SemesterGPA> SemesterGPAs { get; set; }
    }
}
