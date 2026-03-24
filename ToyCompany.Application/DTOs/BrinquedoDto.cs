namespace ToyCompany.Application.DTOs;

public sealed record BrinquedoDto(
    int Id,
    string NomeBrinquedo,
    string TipoBrinquedo,
    int Classificacao,
    string Tamanho,
    decimal Preco);
