

namespace Infrastructure;

using Application.Ports;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;
using Application.Modules.Reports;

public static class DependencyInyection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register HttpClient for Ollama service
        services.AddHttpClient<AiOllamaService>();

        // Register the AI service implementation
        services.AddScoped<IAiServicePort, AiOllamaService>();

        // Register text extraction service
        services.AddScoped<IExtractTextPort, ExtractTextService>();

        // Register report handler
        services.AddScoped<GenerateReportHandler>();

        return services;
    }
}
