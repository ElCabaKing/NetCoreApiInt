
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
            string report = await aiService.GenerateReportAsync(filetext);
            return report;
        }
    }
}