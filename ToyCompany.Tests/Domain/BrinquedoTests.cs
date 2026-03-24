using ToyCompany.Domain.Entities;
using ToyCompany.Domain.Exceptions;

namespace ToyCompany.Tests.Domain;

public sealed class BrinquedoTests
{
    [Fact]
    public void Deve_criar_brinquedo_quando_dados_forem_validos()
    {
        var brinquedo = new Brinquedo("Lego Space", "Blocos", 10, "Medio", 149.90m);

        Assert.Equal("Lego Space", brinquedo.NomeBrinquedo);
        Assert.Equal("Blocos", brinquedo.TipoBrinquedo);
        Assert.Equal(10, brinquedo.Classificacao);
        Assert.Equal("Medio", brinquedo.Tamanho);
        Assert.Equal(149.90m, brinquedo.Preco);
    }

    [Fact]
    public void Deve_lancar_excecao_quando_nome_for_invalido()
    {
        var act = () => new Brinquedo("", "Blocos", 10, "Medio", 149.90m);

        var exception = Assert.Throws<DomainValidationException>(act);
        Assert.Equal("O campo NomeBrinquedo e obrigatorio.", exception.Message);
    }

    [Fact]
    public void Deve_lancar_excecao_quando_classificacao_for_maior_que_catorze()
    {
        var act = () => new Brinquedo("Lego Space", "Blocos", 15, "Medio", 149.90m);

        var exception = Assert.Throws<DomainValidationException>(act);
        Assert.Equal("A classificacao deve estar entre 0 e 14 anos.", exception.Message);
    }

    [Fact]
    public void Deve_lancar_excecao_quando_preco_for_menor_ou_igual_a_zero()
    {
        var act = () => new Brinquedo("Lego Space", "Blocos", 10, "Medio", 0m);

        var exception = Assert.Throws<DomainValidationException>(act);
        Assert.Equal("O preco deve ser maior que zero.", exception.Message);
    }
}
