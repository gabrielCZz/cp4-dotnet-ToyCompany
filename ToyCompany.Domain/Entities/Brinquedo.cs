using ToyCompany.Domain.Exceptions;

namespace ToyCompany.Domain.Entities;

public class Brinquedo
{
    private const int LimiteMaximoClassificacao = 14;

    private Brinquedo()
    {
        NomeBrinquedo = string.Empty;
        TipoBrinquedo = string.Empty;
        Tamanho = string.Empty;
    }

    public Brinquedo(string nomeBrinquedo, string tipoBrinquedo, int classificacao, string tamanho, decimal preco) : this()
    {
        AtualizarDados(nomeBrinquedo, tipoBrinquedo, classificacao, tamanho, preco);
    }

    public int Id { get; private set; }

    public string NomeBrinquedo { get; private set; }

    public string TipoBrinquedo { get; private set; }

    public int Classificacao { get; private set; }

    public string Tamanho { get; private set; }

    public decimal Preco { get; private set; }

    public void AtualizarDados(string nomeBrinquedo, string tipoBrinquedo, int classificacao, string tamanho, decimal preco)
    {
        ValidarTexto(nomeBrinquedo, nameof(NomeBrinquedo), 100);
        ValidarTexto(tipoBrinquedo, nameof(TipoBrinquedo), 60);
        ValidarTexto(tamanho, nameof(Tamanho), 30);
        ValidarClassificacao(classificacao);
        ValidarPreco(preco);

        NomeBrinquedo = nomeBrinquedo.Trim();
        TipoBrinquedo = tipoBrinquedo.Trim();
        Classificacao = classificacao;
        Tamanho = tamanho.Trim();
        Preco = decimal.Round(preco, 2, MidpointRounding.AwayFromZero);
    }

    private static void ValidarTexto(string valor, string campo, int tamanhoMaximo)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new DomainValidationException($"O campo {campo} e obrigatorio.");
        }

        if (valor.Trim().Length > tamanhoMaximo)
        {
            throw new DomainValidationException($"O campo {campo} deve ter no maximo {tamanhoMaximo} caracteres.");
        }
    }

    private static void ValidarClassificacao(int classificacao)
    {
        if (classificacao < 0 || classificacao > LimiteMaximoClassificacao)
        {
            throw new DomainValidationException("A classificacao deve estar entre 0 e 14 anos.");
        }
    }

    private static void ValidarPreco(decimal preco)
    {
        if (preco <= 0)
        {
            throw new DomainValidationException("O preco deve ser maior que zero.");
        }
    }
}
