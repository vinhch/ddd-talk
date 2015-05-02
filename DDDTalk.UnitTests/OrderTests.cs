using System;
using System.Linq;

using DDDTalk.Business.Orders;

using FakeItEasy;

using Xunit;

namespace DDDTalk.UnitTests
{
    public class OrderTests
    {
        #region Rename tests

        [Fact]
        public void AddOrderLineTest()
        {
            var order = new Order();

            order.AddOrderLine(12, 34, "gifts");

            Assert.Equal(1, order.OrderLines.Count());
        }

        [Fact]
        public void AddOrderLineTest2()
        {
            var order = new Order();
            order.AddOrderLine(12, 34, "gifts");

            order.AddOrderLine(14, 2, "more gifts");

            Assert.Equal(3, order.OrderLines.Count());
        }

        #endregion

        #region Structure tests

        [Fact]
        public void AddOrderLine_WhenAddingProductAlreadyInTheOrder_ShouldSumQuantityAndReplaceComment()
        {
            var order = new Order();
            order.AddOrderLine(12, 34, "gifts");
            Assert.Equal(1, order.OrderLines.Count());
            Assert.Equal("gifts", order.OrderLines.ElementAt(0).Comment);

            order.AddOrderLine(14, 2, "more gifts");
            Assert.Equal(2, order.OrderLines.Count());
            Assert.Equal(34, order.OrderLines.ElementAt(0).Quantity);
            Assert.Equal(2, order.OrderLines.ElementAt(1).Quantity);

            order.AddOrderLine(14, 1, "for me");
            Assert.Equal(2, order.OrderLines.Count());
            Assert.Equal(3, order.OrderLines.ElementAt(1).Quantity);
        }

        #endregion

        #region Use Explaining Variables tests

        [Fact]
        public void EmptyOrder_WhenOrderHasOrderLines_ShouldHaveZeroOrderLine()
        {
            // Arrange
            var order = new Order();
            order.AddOrderLine(12345, 1111, "blah");
            order.AddOrderLine(54321, 2222, "more blah");

            // Act
            order.EmptyOrder();

            // Assert
            Assert.Equal(0, order.OrderLines.Count());
        }

        #endregion

        #region Utility methods tests

        [Fact]
        public void AddProductToOrder_ForExistingOrder_ShouldSaveOrderWithTheNewProduct()
        {
            var fakeOrderRepository = A.Fake<IOrderRepository>();

            // Arrange
            var orderService = new OrderService(fakeOrderRepository);

            var existingOrder = new Order { Id = 12 };
            existingOrder.AddOrderLine(1234, 999);
            existingOrder.AddOrderLine(1235, 999);

            A.CallTo(() => fakeOrderRepository.LoadOrder(12))
                .Returns(existingOrder);

            // Act
            orderService.AddProductToOrder(orderId: 12, productId: 34, quantity: 56);

            // Assert
            A.CallTo(() => fakeOrderRepository.SaveOrder(A<Order>.That.Matches(o => 
                o.OrderLines.Count() == 3 && 
                o.OrderLines.ElementAt(2).ProductId == 34 && 
                o.OrderLines.ElementAt(2).Quantity == 56)))
                .MustHaveHappened();
        }

        #endregion
    }
}
