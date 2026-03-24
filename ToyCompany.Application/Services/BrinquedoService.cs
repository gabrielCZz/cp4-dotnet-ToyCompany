using ToyCompany.Application.DTOs;
using ToyCompany.Application.Interfaces;
using ToyCompany.Domain.Entities;

namespace ToyCompany.Application.Services;

public sealed class BrinquedoService : IBrinquedoService
{
    private readonly IBrinquedoRepository _brinquedoRepository;

    public BrinquedoService(IBrinquedoRepository brinquedoRepository)
    {
        _brinquedoRepository = brinquedoRepository;
    }

    public async Task<IReadOnlyCollection<BrinquedoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var brinquedos = await _brinquedoRepository.GetAllAsync(cancellationToken);
        return brinquedos.Select(MapearParaDto).ToArray();
    }

    public async Task<BrinquedoDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var brinquedo = await _brinquedoRepository.GetByIdAsync(id, cancellationToken);
        return brinquedo is null ? null : MapearParaDto(brinquedo);
    }

    public async Task<BrinquedoDto> CreateAsync(CreateBrinquedoRequest request, CancellationToken cancellationToken = default)
    {
        var brinquedo = new Brinquedo(
            request.NomeBrinquedo,
            request.TipoBrinquedo,
            request.Classificacao,
            request.Tamanho,
            request.Preco);

        await _brinquedoRepository.AddAsync(brinquedo, cancellationToken);
        await _brinquedoRepository.SaveChangesAsync(cancellationToken);

        return MapearParaDto(brinquedo);
    }

    public async Task<BrinquedoDto?> UpdateAsync(int id, UpdateBrinquedoRequest request, CancellationToken cancellationToken = default)
    {
        var brinquedo = await _brinquedoRepository.GetByIdAsync(id, cancellationToken);

        if (brinquedo is null)
        {
            return null;
        }

        brinquedo.AtualizarDados(
            request.NomeBrinquedo,
            request.TipoBrinquedo,
            request.Classificacao,
            request.Tamanho,
            request.Preco);

        await _brinquedoRepository.UpdateAsync(brinquedo, cancellationToken);
        await _brinquedoRepository.SaveChangesAsync(cancellationToken);

        return MapearParaDto(brinquedo);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var brinquedo = await _brinquedoRepository.GetByIdAsync(id, cancellationToken);

        if (brinquedo is null)
        {
            return false;
        }

        await _brinquedoRepository.DeleteAsync(brinquedo, cancellationToken);
        await _brinquedoRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static BrinquedoDto MapearParaDto(Brinquedo brinquedo)
    {
        return new BrinquedoDto(
            brinquedo.Id,
            brinquedo.NomeBrinquedo,
            brinquedo.TipoBrinquedo,
            brinquedo.Classificacao,
            brinquedo.Tamanho,
            brinquedo.Preco);
    }
}
