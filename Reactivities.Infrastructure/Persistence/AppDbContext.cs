using Microsoft.EntityFrameworkCore;
using Reactivities.Domain;

namespace Reactivities.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return base.SaveChangesAsync(token);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
