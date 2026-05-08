

namespace Infrastructure;

using Application.Ports;
using Infastructure.Services;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;

public static class DependencyInyection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register the AI service implementation
        services.AddScoped<IAiServicePort, AiOllamaService>();

       

        return services;
    }
}
