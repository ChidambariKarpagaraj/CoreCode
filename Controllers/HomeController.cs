using ECommerceAnalytics.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAnalytics.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HomeController> _logger;
        private readonly DataLoaderService _dataLoaderService;
        private readonly DataRefreshService _dataRefreshService;

        public HomeController(ILogger<HomeController> logger, DataLoaderService dataLoaderService, DataRefreshService dataRefreshService)
        {
            _logger = logger;
            _dataLoaderService = dataLoaderService;
            _dataRefreshService = dataRefreshService;
        }

        [HttpPost("load-data")]
        public IActionResult LoadData([FromBody] string filePath)
        {
            try
            {
                _dataLoaderService.LoadCsvData(filePath);
                return Ok("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error loading data: {ex.Message}");
            }
        }

        [HttpPost("refresh")]
        public IActionResult RefreshData()
        {
            try
            {
                _dataRefreshService.RefreshData();
                return Ok("Data refresh triggered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Data refresh failed: {ex.Message}");
            }
        }

    }
}
