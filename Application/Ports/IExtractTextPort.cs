
namespace Application.Ports
{
    public interface IExtractTextPort
    {
        Task<string> PDFExtractAsync(Stream fileStream);
    }
}