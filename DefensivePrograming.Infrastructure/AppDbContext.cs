using DefensivePrograming.Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DefensivePrograming.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<WithOutUsers> WithOutUsers { get; set; }
    }
}
