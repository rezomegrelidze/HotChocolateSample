using Microsoft.EntityFrameworkCore;

namespace HotChocolateSample.Core
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {
            
        }

        public DbSet<Company> Companies { get; set; }
    }
}