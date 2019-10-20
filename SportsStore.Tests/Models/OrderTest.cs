using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests.Models
{
    public class OrderTest
    {
        private readonly Order _order;
        private readonly Product _p1;
        private readonly Product _p3;

        public OrderTest()        {
            Cart cart = new Cart();
            _p1 = new Product( "Football", 10, null,1);
            Product p2 = new Product( "Short", 5, null, 2);
            _p3 = new Product (  "NotInOrder",  5, null, 3);
            cart.AddLine(_p1, 1);
            cart.AddLine(p2, 10);
            _order = new Order(cart, null, false, "Teststraat 10", new City("9000", "Gent"));
        }

        [Fact]
        public void Total_ReturnsSumOfOrderlines()
        {
               Assert.Equal(60, _order.Total);
        }
        [Fact]
        public void HasOrdered_ProductInOrder_ReturnsTrue()
        {
            Assert.True(_order.HasOrdered(_p1));
        }

        [Fact]
        public void HasOrdered_ProductNotInOrder_ReturnsFalse()
        {
            Assert.False(_order.HasOrdered(_p3));
        }
     
    }
}
