
using System.Text;
using Application.Ports;
using UglyToad.PdfPig;

namespace Infrastructure.Services;

public class ExtractTextService : IExtractTextPort
{
    public Task<string> PDFExtractAsync(Stream stream)
    {
        using var document = PdfDocument.Open(stream);

        var builder = new StringBuilder();

        foreach (var page in document.GetPages())
        {
            builder.AppendLine(page.Text);
        }

        return Task.FromResult(builder.ToString());
    }
}