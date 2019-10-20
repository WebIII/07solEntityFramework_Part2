namespace SportsStore.Models
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price, string description = null, int productId = 0)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Product p)) return false;
            return p.ProductId == ProductId;
        }

        public override int GetHashCode()
        {
            return ProductId;
        }
    }
}