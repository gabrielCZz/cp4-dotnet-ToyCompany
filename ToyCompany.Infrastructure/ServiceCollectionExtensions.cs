using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToyCompany.Application.Interfaces;
using ToyCompany.Infrastructure.Persistence;
using ToyCompany.Infrastructure.Repositories;

namespace ToyCompany.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("OracleConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("A connection string 'OracleConnection' nao foi configurada.");
        }

        services.AddDbContext<ToyCompanyDbContext>(options => options.UseOracle(connectionString));
        services.AddScoped<IBrinquedoRepository, BrinquedoRepository>();

        return services;
    }
}
