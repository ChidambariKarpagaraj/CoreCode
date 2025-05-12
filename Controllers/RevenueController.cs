using Microsoft.AspNetCore.Mvc;
using ECommerceAnalytics.Services;

namespace ECommerceAnalytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly RevenueService _analysisService;

        public RevenueController(RevenueService analysisService)
        {
            _analysisService = analysisService;
        }

        [HttpGet("total-revenue")]
        public IActionResult GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var revenue = _analysisService.GetTotalRevenue(startDate, endDate);
            return Ok(new { TotalRevenue = revenue });
        }

        [HttpGet("total-revenue-by-product")]
        public IActionResult GetTotalRevenueByProduct(int productId, DateTime startDate, DateTime endDate)
        {
            var revenue = _analysisService.GetTotalRevenueByProduct(productId, startDate, endDate);
            return Ok(new { TotalRevenue = revenue });
        }

        [HttpGet("total-revenue-by-category")]
        public IActionResult GetTotalRevenueByCategory(string category, DateTime startDate, DateTime endDate)
        {
            var revenue = _analysisService.GetTotalRevenueByCategory(category, startDate, endDate);
            return Ok(new { TotalRevenue = revenue });
        }

        [HttpGet("total-number-of-customers")]
        public IActionResult GetTotalNumberOfCustomers(DateTime startDate, DateTime endDate)
        {
            var customers = _analysisService.GetTotalNumberOfCustomers(startDate, endDate);
            return Ok(new { TotalCustomers = customers });
        }

        [HttpGet("average-order-value")]
        public IActionResult GetAverageOrderValue(DateTime startDate, DateTime endDate)
        {
            var averageOrderValue = _analysisService.GetAverageOrderValue(startDate, endDate);
            return Ok(new { AverageOrderValue = averageOrderValue });
        }

        [HttpGet("top-n-products")]
        public IActionResult GetTopNProducts(int n, DateTime startDate, DateTime endDate)
        {
            var topProducts = _analysisService.GetTopNProducts(n, startDate, endDate);
            return Ok(new { TopProducts = topProducts });
        }
        [HttpGet("revenue-by-region")]
        public IActionResult GetRevenueByRegion([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var revenueByRegion = _analysisService.GetRevenueByRegion(startDate, endDate);
                return Ok(revenueByRegion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
