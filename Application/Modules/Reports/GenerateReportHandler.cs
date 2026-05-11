
using Application.Ports;
using System.Threading.Tasks;

namespace Application.Modules.Reports
{

    public class GenerateReportHandler(
        IAiServicePort aiService,
        IExtractTextPort extractTextPort
    )
    {
        public async IAsyncEnumerable<string> HandleStreamAsync(Stream fileStream)
        {
            string filetext = await extractTextPort.PDFExtractAsync(fileStream);
            await foreach (var chunk in aiService.GenerateResponseStreamAsync(filetext))
            {
                yield return chunk;
            }
        }
    }
}