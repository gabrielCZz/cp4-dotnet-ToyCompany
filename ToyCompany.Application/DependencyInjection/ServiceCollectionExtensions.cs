using Microsoft.Extensions.DependencyInjection;
using ToyCompany.Application.Interfaces;
using ToyCompany.Application.Services;

namespace ToyCompany.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBrinquedoService, BrinquedoService>();
        return services;
    }
}
