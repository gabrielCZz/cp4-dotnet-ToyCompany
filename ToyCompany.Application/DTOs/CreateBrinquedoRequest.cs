using System.ComponentModel.DataAnnotations;

namespace ToyCompany.Application.DTOs;

public sealed class CreateBrinquedoRequest
{
    [Required]
    [MaxLength(100)]
    public string NomeBrinquedo { get; init; } = string.Empty;

    [Required]
    [MaxLength(60)]
    public string TipoBrinquedo { get; init; } = string.Empty;

    [Range(0, 14)]
    public int Classificacao { get; init; }

    [Required]
    [MaxLength(30)]
    public string Tamanho { get; init; } = string.Empty;

    [Range(0.01, 999999.99)]
    public decimal Preco { get; init; }
}
