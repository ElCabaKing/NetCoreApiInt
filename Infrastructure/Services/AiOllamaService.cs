namespace Infastructure.Services
{
    using Application.Ports;
    using System.Threading.Tasks;

    public class AiOllamaService : IAiServicePort
    {
        public Task<string> GenerateResponseAsync(string prompt)
        {
            // Implement the logic to call the Ollama API and generate a response based on the prompt
            throw new NotImplementedException();
        }

        public Task<string> GenerateReportAsync(string text)
        {
            // Implement the logic to call the Ollama API and generate a report based on the input text
            throw new NotImplementedException();
        }
    }
}