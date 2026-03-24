using System.Reflection;
using ToyCompany.Application.DTOs;
using ToyCompany.Application.Interfaces;
using ToyCompany.Application.Services;
using ToyCompany.Domain.Entities;

namespace ToyCompany.Tests.Application;

public sealed class BrinquedoServiceTests
{
    [Fact]
    public async Task CreateAsync_deve_adicionar_brinquedo_e_retornar_dto()
    {
        var repository = new FakeBrinquedoRepository();
        var service = new BrinquedoService(repository);

        var result = await service.CreateAsync(new CreateBrinquedoRequest
        {
            NomeBrinquedo = "Autorama Max",
            TipoBrinquedo = "Corrida",
            Classificacao = 12,
            Tamanho = "Grande",
            Preco = 199.99m
        });

        Assert.Equal(1, result.Id);
        Assert.Single(repository.Items);
        Assert.Equal("Autorama Max", result.NomeBrinquedo);
    }

    [Fact]
    public async Task UpdateAsync_deve_atualizar_brinquedo_existente()
    {
        var repository = new FakeBrinquedoRepository();
        repository.Seed(new Brinquedo("Boneca Fashion", "Boneca", 7, "Pequeno", 79.90m), 10);
        var service = new BrinquedoService(repository);

        var result = await service.UpdateAsync(10, new UpdateBrinquedoRequest
        {
            NomeBrinquedo = "Boneca Fashion Plus",
            TipoBrinquedo = "Boneca",
            Classificacao = 8,
            Tamanho = "Medio",
            Preco = 99.90m
        });

        Assert.NotNull(result);
        Assert.Equal(10, result!.Id);
        Assert.Equal("Boneca Fashion Plus", result.NomeBrinquedo);
        Assert.Equal("Medio", repository.Items.Single().Tamanho);
    }

    [Fact]
    public async Task DeleteAsync_deve_retornar_false_quando_id_nao_existir()
    {
        var repository = new FakeBrinquedoRepository();
        var service = new BrinquedoService(repository);

        var result = await service.DeleteAsync(999);

        Assert.False(result);
    }

    [Fact]
    public async Task GetByIdAsync_deve_retornar_null_quando_brinquedo_nao_existir()
    {
        var repository = new FakeBrinquedoRepository();
        var service = new BrinquedoService(repository);

        var result = await service.GetByIdAsync(50);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_deve_retornar_lista_ordenada_por_id()
    {
        var repository = new FakeBrinquedoRepository();
        repository.Seed(new Brinquedo("Quebra Cabeca", "Educativo", 10, "Medio", 49.90m), 2);
        repository.Seed(new Brinquedo("Jogo da Memoria", "Educativo", 6, "Pequeno", 29.90m), 1);
        var service = new BrinquedoService(repository);

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result.First().Id);
        Assert.Equal(2, result.Last().Id);
    }

    private sealed class FakeBrinquedoRepository : IBrinquedoRepository
    {
        private int _nextId = 1;

        public List<Brinquedo> Items { get; } = [];

        public Task<IReadOnlyCollection<Brinquedo>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            IReadOnlyCollection<Brinquedo> result = Items.OrderBy(x => x.Id).ToArray();
            return Task.FromResult(result);
        }

        public Task<Brinquedo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Items.SingleOrDefault(x => x.Id == id));
        }

        public Task AddAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default)
        {
            SetId(brinquedo, _nextId++);
            Items.Add(brinquedo);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Brinquedo brinquedo, CancellationToken cancellationToken = default)
        {
            Items.Remove(brinquedo);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Seed(Brinquedo brinquedo, int id)
        {
            SetId(brinquedo, id);
            Items.Add(brinquedo);

            if (id >= _nextId)
            {
                _nextId = id + 1;
            }
        }

        private static void SetId(Brinquedo brinquedo, int id)
        {
            var property = typeof(Brinquedo).GetProperty(nameof(Brinquedo.Id), BindingFlags.Instance | BindingFlags.Public);
            property!.SetValue(brinquedo, id);
        }
    }
}
