using Microsoft.EntityFrameworkCore;
using ToyCompany.Application.Interfaces;
using ToyCompany.Domain.Entities;
using ToyCompany.Infrastructure.Persistence;

namespace ToyCompany.Infrastructure.Repositories;

public sealed class BrinquedoRepository : IBrinquedoRepository
{
    private readonly ToyCompanyDbContext _dbContext;

    public BrinquedoRepository(ToyCompanyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Brinquedo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Brinquedos
            .AsNoTracking()
            .OrderBy(b => b.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Brinquedo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Brinquedos.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task AddAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default)
    {
        await _dbContext.Brinquedos.AddAsync(brinquedo, cancellationToken);
    }

    public Task UpdateAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default)
    {
        _dbContext.Brinquedos.Update(brinquedo);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default)
    {
        _dbContext.Brinquedos.Remove(brinquedo);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
