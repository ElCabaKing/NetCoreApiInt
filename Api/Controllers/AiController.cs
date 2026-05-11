namespace Api.Controllers
{
    using Api.Request;
    using Application.Modules.Reports;
    using Application.Ports;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AiController(
        GenerateReportHandler generateReportHandler) : ControllerBase
    {
        [HttpPost("stream-report")]
        public async Task StreamReport([FromForm] IFormFile file)
        {
            Response.ContentType = "text/plain; charset=utf-8";

            await using var fileStream = file.OpenReadStream();

            await foreach (var chunk in generateReportHandler.HandleStreamAsync(fileStream))
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(chunk);
                await Response.Body.WriteAsync(bytes, 0, bytes.Length, HttpContext.RequestAborted);
                await Response.Body.FlushAsync(HttpContext.RequestAborted);
            }
        }
    }
}
