namespace ECommerceAnalytics.Models
{
    public class Product
    {
       public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
