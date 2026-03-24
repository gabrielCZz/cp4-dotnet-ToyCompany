using ToyCompany.Application.DTOs;

namespace ToyCompany.Application.Interfaces;

public interface IBrinquedoService
{
    Task<IReadOnlyCollection<BrinquedoDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<BrinquedoDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BrinquedoDto> CreateAsync(CreateBrinquedoRequest request, CancellationToken cancellationToken = default);

    Task<BrinquedoDto?> UpdateAsync(int id, UpdateBrinquedoRequest request, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
