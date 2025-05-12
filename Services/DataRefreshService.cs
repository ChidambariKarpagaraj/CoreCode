using ECommerceAnalytics.Data;
using ECommerceAnalytics.Models;

namespace ECommerceAnalytics.Services
{
    public class DataRefreshService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DataRefreshService> _logger;

        public DataRefreshService(AppDbContext context, ILogger<DataRefreshService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void RefreshData()
        {
            try
            {
                _logger.LogInformation("Starting data refresh...");

                var newSalesData = LoadDataFromCSV("path_to_your_data.csv");

                _context.Sales.AddRange(newSalesData);
                _context.SaveChanges();

                _logger.LogInformation("Data refresh completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Data refresh failed: {ex.Message}");
            }
        }

        private IEnumerable<Sale> LoadDataFromCSV(string filePath)
        {
            var salesData = new List<Sale>();

            salesData.Add(new Sale
            {
                OrderId = 1001,
                ProductId = 123,
                CustomerId = 456,
                Region = "North America",
                DateOfSale = DateTime.Parse("2023-12-15"),
                QuantitySold = 2,
                UnitPrice = 180.00m,
                Discount = 0.1M,
                ShippingCost = 10.00m,
                PaymentMethod = "Credit Card"
            });

            return salesData;
        }
    }
}
