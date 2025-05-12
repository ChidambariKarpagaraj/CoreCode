using ECommerceAnalytics.Data;

namespace ECommerceAnalytics.Services
{
    public class RevenueService
    {
        private readonly AppDbContext _context;

        public RevenueService(AppDbContext context)
        {
            _context = context;
        }

        public decimal GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(s => s.DateOfSale >= startDate && s.DateOfSale <= endDate)
                .Sum(s => s.QuantitySold * s.UnitPrice * (1 - (decimal)s.Discount) + s.ShippingCost);
        }

        public decimal GetTotalRevenueByProduct(int productId, DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(s => s.ProductId == productId && s.DateOfSale >= startDate && s.DateOfSale <= endDate)
                .Sum(s => s.QuantitySold * s.UnitPrice * (1 - (decimal)s.Discount) + s.ShippingCost);
        }

        public decimal GetTotalRevenueByCategory(string category, DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(s => s.Product.Category == category && s.DateOfSale >= startDate && s.DateOfSale <= endDate)
                .Sum(s => s.QuantitySold * s.UnitPrice * (1 - (decimal)s.Discount) + s.ShippingCost);
        }
        public Dictionary<string, decimal> GetRevenueByRegion(DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(s => s.DateOfSale >= startDate && s.DateOfSale <= endDate)
                .GroupBy(s => s.Region)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(s => s.QuantitySold * s.UnitPrice * (1 - (decimal)s.Discount))
                );
        }
        public int GetTotalNumberOfCustomers(DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(s => s.DateOfSale >= startDate && s.DateOfSale <= endDate)
                .Select(s => s.CustomerId)
                .Distinct()
                .Count();
        }

        public decimal GetAverageOrderValue(DateTime startDate, DateTime endDate)
        {
            var totalRevenue = GetTotalRevenue(startDate, endDate);
            var totalOrders = _context.Sales
                .Where(s => s.DateOfSale >= startDate && s.DateOfSale <= endDate)
                .Select(s => s.OrderId)
                .Distinct()
                .Count();
            return totalOrders == 0 ? 0 : totalRevenue / totalOrders;
        }

        public IEnumerable<string> GetTopNProducts(int n, DateTime startDate, DateTime endDate)
        {
            return _context.Sales
                .Where(s => s.DateOfSale >= startDate && s.DateOfSale <= endDate)
                .GroupBy(s => s.Product.Name)
                .OrderByDescending(g => g.Sum(s => s.QuantitySold))
                .Take(n)
                .Select(g => g.Key)
                .ToList();
        }
    }
}

