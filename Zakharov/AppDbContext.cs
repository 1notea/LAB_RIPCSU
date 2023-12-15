using Microsoft.EntityFrameworkCore;
using Zakharov.Models;
namespace Zakharov
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Journal> Journals { get; set; }
    }
}
