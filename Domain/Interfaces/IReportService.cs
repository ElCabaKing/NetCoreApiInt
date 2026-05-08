
namespace Domain.Interfaces
{
    public interface IReportService
    {
        Task<string> GenerateReportAsync(string text);
    }
}