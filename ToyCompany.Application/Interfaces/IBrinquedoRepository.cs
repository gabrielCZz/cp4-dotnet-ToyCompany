using ToyCompany.Domain.Entities;

namespace ToyCompany.Application.Interfaces;

public interface IBrinquedoRepository
{
    Task<IReadOnlyCollection<Brinquedo>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Brinquedo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task AddAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default);

    Task UpdateAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default);

    Task DeleteAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
