using Microsoft.EntityFrameworkCore;

namespace Shared.Data.Entities
{
    public class RtlDbContext : DbContext
    {
        public RtlDbContext(DbContextOptions<RtlDbContext> options) : base(options)
        {
        }

        public DbSet<ShowEntity> Shows { get; set; }
    }
}
