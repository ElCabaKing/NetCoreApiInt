namespace Api.Controllers
{
    using Application.Ports;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly IAiServicePort _aiService;
        private readonly ILogger<AiController> _logger;

        public AiController(IAiServicePort aiService, ILogger<AiController> logger)
        {
            _aiService = aiService;
            _logger = logger;
        }

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
        public async Task<IActionResult> GenerateReport([FromBody] ReportRequest request)
        {
            try
            {
            
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating report: {ex.Message}");
                return StatusCode(500, new { error = "Failed to generate report", details = ex.Message });
            }
        }
    }

    public class MessageRequest
    {
        public string Prompt { get; set; }
    }

    public class ReportRequest
    {
        public File file { get; set; }
    }
}
