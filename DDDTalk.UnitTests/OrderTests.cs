using System;
using System.Linq;

using DDDTalk.Business.Orders;

using FakeItEasy;

using Xunit;
using Xunit.Abstractions;

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

        #region Only pertinent literals tests

        public class OrderServiceTestsWithoutAutoFixture
        {
            private readonly ITestOutputHelper output;

            private readonly IOrderRepository fakeOrderRepository;
            private readonly OrderService orderService;

            public OrderServiceTestsWithoutAutoFixture(ITestOutputHelper output)
            {
                this.output = output;

                this.fakeOrderRepository = A.Fake<IOrderRepository>();
                this.orderService = new OrderService(this.fakeOrderRepository);
            }

            [Fact]
            public void AddProductToOrder_ForExistingOrder_ShouldSaveOrderWithTheNewProduct()
            {
                const int OrderId = 12;
                const int NumberOfLine = 2;

                this.output.WriteLine("OrderId: {0}", OrderId);
                // this.output.WriteLine("ProductId: {0}", productId);
                // this.output.WriteLine("Quantity: {0}", quantity);

                // Arrange
                var existingOrder = CreateSomeOrder(OrderId, NumberOfLine);

                this.ArrangeOrderRepositoryLoadOrderReturns(existingOrder);

                // Act
                this.orderService.AddProductToOrder(OrderId, productId: 34, quantity: 56);

                // Assert
                this.AssertOrderRepositorySaveOrderWith(expectedCount: NumberOfLine + 1, expectedNewProductId: 34, expectedNewQuantity: 56);
            }

            private static Order CreateSomeOrder(int orderId, int numberOfLine)
            {
                var existingOrder = new Order { Id = orderId };
                for (var i = 0; i < numberOfLine; i++)
                {
                    existingOrder.AddOrderLine(12356 + i, 999);
                }

                return existingOrder;
            }

            private void ArrangeOrderRepositoryLoadOrderReturns(Order existingOrder)
            {
                A.CallTo(() => this.fakeOrderRepository.LoadOrder(existingOrder.Id)).Returns(existingOrder);
            }

            private void AssertOrderRepositorySaveOrderWith(int expectedCount, int expectedNewProductId, int expectedNewQuantity)
            {
                A.CallTo(() => this.fakeOrderRepository.SaveOrder(A<Order>.That.Matches(o =>
                    o.OrderLines.Count() == expectedCount &&
                    o.OrderLines.ElementAt(expectedCount - 1).ProductId == expectedNewProductId &&
                    o.OrderLines.ElementAt(expectedCount - 1).Quantity == expectedNewQuantity)))
                    .MustHaveHappened();
            }
        }

        #endregion
    }
}
