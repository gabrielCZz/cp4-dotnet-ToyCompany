using Microsoft.EntityFrameworkCore;
using ToyCompany.Domain.Entities;

namespace ToyCompany.Infrastructure.Persistence;

public sealed class ToyCompanyDbContext : DbContext
{
    public ToyCompanyDbContext(DbContextOptions<ToyCompanyDbContext> options) : base(options)
    {
    }

    public DbSet<Brinquedo> Brinquedos => Set<Brinquedo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToyCompanyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
