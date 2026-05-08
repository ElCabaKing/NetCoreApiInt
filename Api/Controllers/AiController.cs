namespace Api.Controllers
{
    using Api.Request;
    using Application.Modules.Reports;
    using Application.Ports;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AiController(
        IAiServicePort aiService,
        ILogger<AiController> logger,
        GenerateReportHandler generateReportHandler) : ControllerBase
    {
        private readonly IAiServicePort _aiService = aiService;
        private readonly ILogger<AiController> _logger = logger;
        private readonly GenerateReportHandler _generateReportHandler = generateReportHandler;

        /// <summary>
        /// Send a message/prompt to Ollama and get a response
        /// </summary>
        /// <param name="request">The message request containing the prompt</param>
        /// <returns>The AI-generated response</returns>
        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromBody] MessageRequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Prompt))
            {
                return BadRequest(new { error = "Prompt cannot be empty" });
            }

            try
            {
                _logger.LogInformation($"Sending message to Ollama: {request.Prompt}");
                var response = await _aiService.GenerateResponseAsync(request.Prompt);

                return Ok(new
                {
                    success = true,
                    prompt = request.Prompt,
                    response = response
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling Ollama: {ex.Message}");
                return StatusCode(500, new { error = "Failed to generate response", details = ex.Message });
            }
        }

        /// <summary>
        /// Generate a report based on provided text
        /// </summary>
        /// <param name="request">The report request containing text to generate report from</param>
        /// <returns>The generated report</returns>
        [HttpPost("report")]
        public async Task<IActionResult> GenerateReport([FromForm] ReportRequest request)
        {
            try
            {
                return Ok(new
                {
                    success = true,
                    report = await _generateReportHandler.HandleAsync(request.File.OpenReadStream())
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating report: {ex.Message}");
                return StatusCode(500, new { error = "Failed to generate report", details = ex.Message });
            }
        }
    }
}
