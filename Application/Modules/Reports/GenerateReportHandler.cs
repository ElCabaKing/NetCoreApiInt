
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
            string filetext = extractTextPort.PDFExtractAsync(fileStream).Result;
            string report = await aiService.GenerateReportAsync(filetext);
            return report;
        }
    }
}