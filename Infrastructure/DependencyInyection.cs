

namespace Infrastructure;

using Application.Ports;
using Infastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;

public static class DependencyInyection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register HttpClient for Ollama service
        services.AddHttpClient<AiOllamaService>();

        // Register the AI service implementation
        services.AddScoped<IAiServicePort, AiOllamaService>();

        return services;
    }
}
