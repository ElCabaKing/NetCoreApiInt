namespace Infrastructure.Services;

using Application.Ports;
using Infrastructure.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

public class AiOllamaService : IAiServicePort
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AiOllamaService> _logger;
    private readonly string _ollamaUrl;
    private readonly string _model;

    public AiOllamaService(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<AiOllamaService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;

        _ollamaUrl =
            configuration[ConfigurationConstants.OllamaUrl]
            ?? "http://localhost:11434";

        _model =
            configuration[ConfigurationConstants.OllamaModelName]
            ?? "llama2";
    }

    public async IAsyncEnumerable<string> GenerateResponseStreamAsync(
        string prompt)
    {
        var requestBody = new
        {
            model = _model,
            system = ModelConstants.SystemPrompt,
            prompt,
            stream = true
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_ollamaUrl}/api/generate"
        );

        request.Content = jsonContent;

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead
        );

        response.EnsureSuccessStatusCode();

        await using var stream =
            await response.Content.ReadAsStreamAsync();

        using var reader = new StreamReader(stream);

        while (await reader.ReadLineAsync() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var jsonResponse =
                JsonSerializer.Deserialize<JsonElement>(line);

            if (!jsonResponse.TryGetProperty(
                    "response",
                    out var responseElement))
            {
                continue;
            }

            var chunk = responseElement.GetString();

            if (string.IsNullOrWhiteSpace(chunk))
                continue;

            yield return chunk;
        }
    }

    public async Task<string> GenerateResponseAsync(string prompt)
    {
        var builder = new StringBuilder();

        await foreach (var chunk in GenerateResponseStreamAsync(prompt))
        {
            builder.Append(chunk);
        }

        return builder.ToString();
    }

    public Task<string> GenerateReportAsync(string text)
        => GenerateResponseAsync(text);
}