using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests.Models
{
    public class CategoryTest
    {
        private Category _category;
        private Product _football;

        public CategoryTest()
        {
            _category = new Category("Soccer");
            _football = new Product("Football", 10);
        }

        [Fact]
        public void Category_StartsEmpty()
        {
            Assert.Equal(0, _category.Products.Count);
        }

        [Fact]
        public void Add_NewProduct_AddsProduct()
        {
            _category.AddProduct("Football", 10, null);
            Assert.Equal(1, _category.Products.Count);
        }

        [Fact]
        public void Add_ProductNotInCategory_AddsProduct()
        {
            _category.AddProduct(_football);
            Assert.Equal(1, _category.Products.Count);
        }

        [Fact]
        public void Add_ProductInCategory_DoesnotAddProduct()
        {
            _category.AddProduct(_football);
            _category.AddProduct(_football);
            Assert.Equal(1, _category.Products.Count);
        }

        [Fact]
        public void Remove_RemovesProduct()
        {
            _category.AddProduct(_football);
            _category.RemoveProduct(_football);
            Assert.Equal(0, _category.Products.Count);
        }

        [Fact]
        public void FindProduct_ProductInCategory_ReturnsProduct()
        {
            _category.AddProduct("Football", 10, null);
            Assert.NotNull(_category.FindProduct("Football"));
        }

        [Fact]
        public void FindProduct_ProductNotInCategory_ReturnsNull()
        {
            Assert.Null(_category.FindProduct("Football"));
        }
    }
}
