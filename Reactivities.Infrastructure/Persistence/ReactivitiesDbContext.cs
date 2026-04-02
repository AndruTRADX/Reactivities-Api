using System;
using Microsoft.EntityFrameworkCore;

namespace Reactivities.Infrastructure.Persistence;

public class ReactivitiesDbContext(DbContextOptions<ReactivitiesDbContext> options) : DbContext(options)
{
    public override Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return base.SaveChangesAsync(token);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
