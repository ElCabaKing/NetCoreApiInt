namespace Infrastructure.Services
{
    using Application.Ports;
    using Infrastructure.Constants;
    using Microsoft.Extensions.Configuration;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class AiOllamaService : IAiServicePort
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _ollamaUrl;

        public AiOllamaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _ollamaUrl = _configuration[ConfigurationConstants.OllamaUrl] ?? "http://localhost:11434";
        }

        public async Task<string> GenerateResponseAsync(string prompt)
        {
            var model = _configuration[ConfigurationConstants.OllamaModelName] ?? "llama2";
            var requestBody = new
            {
                model = model,
                prompt = prompt,
                stream = false
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_ollamaUrl}/api/generate", jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Ollama API returned status code: {response.StatusCode}. Body: {responseContent}");
            }

            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
            return jsonResponse.GetProperty("response").GetString() ?? "No response generated";
        }

        public async Task<string> GenerateReportAsync(string text)
        {
            var prompt = $"Generate a professional report based on the following text:\n\n{text}";
            return await GenerateResponseAsync(prompt);
        }
    }
}