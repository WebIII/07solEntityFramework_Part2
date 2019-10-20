namespace SportsStore.Models
{
    public class CategoryProduct
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }

        protected CategoryProduct() { }

        public CategoryProduct(Category category, Product product) : this()
        {
            Category = category;
            Product = product;
            CategoryId = category.CategoryId;
            ProductId = product.ProductId;
        }
    }
}