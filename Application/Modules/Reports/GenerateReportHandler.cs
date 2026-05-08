
using Application.Ports;
using System.Threading.Tasks;

namespace Application.Modules.Reports
{

    public class GenerateReportHandler(
        IAiServicePort aiService,
        IExtractTextPort extractTextPort
    )
    {

        public async Task<string> HandleAsync(Stream fileStream)
        {
            string filetext = await extractTextPort.PDFExtractAsync(fileStream);
            Console.WriteLine($"Extracted text: {filetext}");
            string report = await aiService.GenerateReportAsync(filetext);
            return filetext;
        }
    }
}