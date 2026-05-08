namespace Application.Ports
{
    public interface IAiServicePort
    {
        Task<string> GenerateResponseAsync(string prompt);
        Task<string> GenerateReportAsync(string text);
    }
}