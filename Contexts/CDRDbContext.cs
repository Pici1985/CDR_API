using CDR_API.Entities;
using Microsoft.EntityFrameworkCore;


namespace CDR_API.Contexts
{
    public class CDRDbContext : DbContext
    {
        public CDRDbContext(DbContextOptions<CDRDbContext> options) : base(options)
        {
        }

        public DbSet<TestTable> TestTable { get; set; }
        public DbSet<CDR_Table> CDR_Table { get; set; }
    }
}
