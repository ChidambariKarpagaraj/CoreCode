using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using ECommerceAnalytics.Models;
using ECommerceAnalytics.Data;

namespace ECommerceAnalytics.Services
{
    public class DataLoaderService
    {
        private readonly AppDbContext _context;

        public DataLoaderService(AppDbContext context)
        {
            _context = context;
        }


        public void LoadCsvData(string filePath)
        {
            var salesData = ReadCsv(filePath);
            SaveDataToDatabase(salesData);
        }


        private List<SaleData> ReadCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<SaleData>().ToList();
            }
        }


        private void SaveDataToDatabase(List<SaleData> saleData)
        {
            foreach (var data in saleData)
            {

                var customer = _context.Customers
                    .FirstOrDefault(c => c.CustomerId == data.CustomerId);
                if (customer == null)
                {
                    customer = new Customer
                    {
                        CustomerId = data.CustomerId,
                        Name = data.CustomerName,
                        Email = data.CustomerEmail,
                        Address = data.CustomerAddress
                    };
                    _context.Customers.Add(customer);
                }

                var product = _context.Products
                    .FirstOrDefault(p => p.ProductId == data.ProductId);
                if (product == null)
                {
                    product = new Product
                    {
                        ProductId = data.ProductId,
                        Name = data.ProductName,
                        Category = data.Category,
                         UnitPrice= data.UnitPrice
                    };
                    _context.Products.Add(product);
                }

                var sale = new Sale
                {
                    ProductId = data.ProductId,
                    CustomerId = data.CustomerId,
                    Region = data.Region,
                    DateOfSale = data.DateOfSale,
                    QuantitySold = data.QuantitySold,
                    UnitPrice = data.UnitPrice,
                    Discount = data.Discount,
                    ShippingCost = data.ShippingCost,
                    PaymentMethod = data.PaymentMethod
                };

                _context.Sales.Add(sale);
            }

            _context.SaveChanges();
        }
    }

    // This class holds the parsed CSV data
    public class SaleData
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Region { get; set; }
        public DateTime DateOfSale { get; set; }
        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public string PaymentMethod { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
    }
}