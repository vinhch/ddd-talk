using System;
using System.Collections.Generic;
using System.Linq;

using DDDTalk.Business.Orders;

using FakeItEasy;

using Fasterflect;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Ploeh.AutoFixture;

using Xunit;
using Xunit.Abstractions;

namespace DDDTalk.UnitTests
{
    public class OrderTestsFinal
    {
        #region Rename tests

        [Fact]
        public void AddOrderLine_WhenOrderIsEmpty_ShouldHaveOneOrderLine()
        {
            var order = new Order();

            order.AddOrderLine(12, 34, "gifts");

            Assert.Equal(1, order.OrderLines.Count());
        }

        [Fact]
        public void AddOrderLine_WhenAlreadyOneOrderLine_ShouldHaveTwoOrderLine()
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
            // Arrange
            var order = new Order();
            order.AddOrderLine(12, 34, "gifts");
            order.AddOrderLine(14, 2, "more gifts");

            // Act
            order.AddOrderLine(14, 1, "for me");

            // Assert
            Assert.Equal(2, order.OrderLines.Count());

            Assert.Equal(12, order.OrderLines.ElementAt(0).ProductId);
            Assert.Equal(34, order.OrderLines.ElementAt(0).Quantity);
            Assert.Equal("gifts", order.OrderLines.ElementAt(0).Comment);

            Assert.Equal(14, order.OrderLines.ElementAt(1).ProductId);
            Assert.Equal(3, order.OrderLines.ElementAt(1).Quantity);
            Assert.Equal("for me", order.OrderLines.ElementAt(1).Comment);
        }

        #endregion

        #region Use Explaining Variables tests

        [Fact]
        public void EmptyOrder_WhenOrderHasOrderLines_ShouldHaveZeroOrderLine()
        {
            const int ProductId1 = 1;
            const int ProductId2 = 2;
            const int AnyQuantity = 3;
            const string AnyComment = "any comment";

            // Arrange
            var order = new Order();
            order.AddOrderLine(ProductId1, AnyQuantity, AnyComment);
            order.AddOrderLine(ProductId2, AnyQuantity, AnyComment);

            // Act
            order.EmptyOrder();

            // Assert
            Assert.Equal(0, order.OrderLines.Count());
        }

        #endregion

        #region Utility methods tests

        public class OrderServiceTests
        {
            private readonly IOrderRepository fakeOrderRepository;
            private readonly OrderService orderService;

            public OrderServiceTests()
            {
                this.fakeOrderRepository = A.Fake<IOrderRepository>();

                this.orderService = new OrderService(this.fakeOrderRepository);
            }

            [Fact]
            public void AddProductToOrder_ForExistingOrder_ShouldSaveOrderWithTheNewProduct()
            {
                const int OrderId = 12;
                const int NumberOfLine = 2;

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
                    existingOrder.AddOrderLine(productId: 123456 + i, quantity: 999);
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

        #region Only pertinent literals tests

        public class OrderServiceTestsWithAutoFixture
        {
            private readonly ITestOutputHelper output;
            private readonly Fixture fixture;

            private readonly IOrderRepository fakeOrderRepository;
            private readonly OrderService orderService;

            public OrderServiceTestsWithAutoFixture(ITestOutputHelper output)
            {
                this.output = output;
                this.fixture = new Fixture();

                this.fakeOrderRepository = A.Fake<IOrderRepository>();

                this.orderService = new OrderService(this.fakeOrderRepository);
            }

            [Fact]
            public void AddProductToOrder_ForExistingOrder_ShouldSaveOrderWithTheNewProduct()
            {
                var orderId = this.fixture.Create<int>();
                var productId = this.fixture.Create<int>();
                var quantity = this.fixture.Create<int>();
                const int NumberOfLine = 2;

                // Arrange
                var existingOrder = this.CreateSomeOrder(orderId, NumberOfLine);

                this.ArrangeOrderRepositoryLoadOrderReturns(existingOrder);

                // Act
                this.orderService.AddProductToOrder(orderId, productId: productId, quantity: quantity);

                // Assert
                this.AssertOrderRepositorySaveOrderWith(expectedCount: NumberOfLine + 1, expectedNewProductId: productId, expectedNewQuantity: quantity);
            }

            private Order CreateSomeOrder(int orderId, int numberOfLine)
            {
                var existingOrder = new Order { Id = orderId };
                for (var i = 0; i < numberOfLine; i++)
                {
                    existingOrder.AddOrderLine(this.fixture.Create<int>(), this.fixture.Create<int>());
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

        #region Easy to understand failure message

        public class OrderServiceTestsWithClearOutput
        {
            private readonly ITestOutputHelper output;
            private readonly Fixture fixture;

            private readonly IOrderRepository fakeOrderRepository;
            private readonly OrderService orderService;

            public OrderServiceTestsWithClearOutput(ITestOutputHelper output)
            {
                this.output = output;
                this.fixture = new Fixture();

                this.fakeOrderRepository = A.Fake<IOrderRepository>();

                this.orderService = new OrderService(this.fakeOrderRepository);
            }

            [Fact]
            public void AddProductToOrder_ForExistingOrder_ShouldSaveOrderWithTheNewProduct()
            {
                var orderId = this.fixture.Create<int>();
                var productId = this.fixture.Create<int>();
                var quantity = this.fixture.Create<int>();
                const int NumberOfLine = 2;

                // Arrange
                var existingOrder = this.CreateSomeOrder(orderId, NumberOfLine);

                this.ArrangeOrderRepositoryLoadOrderReturns(existingOrder);

                // Act
                this.orderService.AddProductToOrder(orderId, productId, quantity + 1);

                // Assert
                this.AssertOrderRepositorySaveOrderWith(expectedCount: NumberOfLine + 1, expectedNewProductId: productId, expectedNewQuantity: quantity);
            }

            private Order CreateSomeOrder(int orderId, int numberOfLine)
            {
                var existingOrder = new Order { Id = orderId };
                for (var i = 0; i < numberOfLine; i++)
                {
                    existingOrder.AddOrderLine(this.fixture.Create<int>(), this.fixture.Create<int>());
                }

                return existingOrder;
            }

            private void ArrangeOrderRepositoryLoadOrderReturns(Order existingOrder)
            {
                A.CallTo(() => this.fakeOrderRepository.LoadOrder(existingOrder.Id)).Returns(existingOrder);
            }

            private void AssertOrderRepositorySaveOrderWith(int expectedCount, int expectedNewProductId, int expectedNewQuantity)
            {
                A.CallTo(() => this.fakeOrderRepository.SaveOrder(A<Order>.That.Matches(actualOrder =>
                    this.OrderEquals(expectedCount, expectedNewProductId, expectedNewQuantity, actualOrder, "SaveOrder"))))
                    .MustHaveHappened();
            }

            private bool OrderEquals(int expectedCount, int expectedNewProductId, int expectedNewQuantity, Order actualOrder, string messageSuffix)
            {
                var result = expectedCount == actualOrder.OrderLines.Count()
                    && expectedNewProductId == actualOrder.OrderLines.Last().ProductId
                    && expectedNewQuantity == actualOrder.OrderLines.Last().Quantity;

                if (!result)
                {
                    this.output.WriteLine("Expected ProductId: {0}", expectedNewProductId);
                    this.output.WriteLine("Expected Quantity: {0}", expectedNewQuantity);

                    this.output.WriteLine("Actual Order: {0}", actualOrder.ToString());
                    Assert.True(result, "Orders doesn't match on " + messageSuffix);
                }

                return result;
            }
        }

        #endregion

        #region Compare state

        public class OrderServiceTestsWithEquals
        {
            private readonly Fixture fixture;

            private readonly IOrderRepository fakeOrderRepository;
            private readonly OrderService orderService;

            public OrderServiceTestsWithEquals()
            {
                this.fixture = new Fixture();

                this.fakeOrderRepository = A.Fake<IOrderRepository>();

                this.orderService = new OrderService(this.fakeOrderRepository);
            }

            [Fact]
            public void AddProductToOrder_ForExistingOrder_ShouldSaveOrderWithTheNewProduct()
            {
                var productId = this.fixture.Create<int>();
                var quantity = this.fixture.Create<int>();

                // Arrange
                var existingOrder = this.Create<Order>();

                var expectedOrder = CreateFrom(existingOrder, o => 
                    o.OrderLines.Add(this.Create<OrderLine>(ol =>
                    {
                        ol.ProductId = productId;
                        ol.Quantity = quantity;
                    })));

                this.ArrangeOrderRepositoryLoadOrderReturns(existingOrder);

                // Act
                this.orderService.AddProductToOrder(existingOrder.Id, productId, quantity);

                // Assert
                this.AssertOrderRepositorySaveOrderWith(expectedOrder);
            }

            private T Create<T>(Action<T> action = null) where T : class, new()
            {
                var result = this.fixture.Create<T>();

                if (action != null)
                {
                    action.Invoke(result);
                }

                return result;
            }

            private static T CreateFrom<T>(T template, Action<T> action = null) where T : class, new()
            {
                var result = template.DeepClone();

                if (action != null)
                {
                    action.Invoke(result);
                }

                return result;
            }

            private void ArrangeOrderRepositoryLoadOrderReturns(Order existingOrder)
            {
                A.CallTo(() => this.fakeOrderRepository.LoadOrder(existingOrder.Id)).Returns(existingOrder);
            }

            private void AssertOrderRepositorySaveOrderWith(Order expectedOrder)
            {
                A.CallTo(() => this.fakeOrderRepository.SaveOrder(A<Order>.That.Matches(actualOrder =>
                    OrderEquals(expectedOrder, actualOrder, "For OrderRepository.SaveOrder"))))
                    .MustHaveHappened();
            }

            private static bool OrderEquals(Order expectedOrder, Order actualOrder, string messageSuffix)
            {
                expectedOrder.OrderLines.ForEach(ol => ol.LineId = Guid.Empty);
                actualOrder.OrderLines.ForEach(ol => ol.LineId = Guid.Empty);
                AssertEqualWithJsonOutput(expectedOrder, actualOrder, messageSuffix);

                return true;
            }

            private static void AssertEqualWithJsonOutput<T>(T expectedObject, T actualObject, string message = "")
            {
                //Assert.Equal(JsonConvert.SerializeObject(expectedObject), JsonConvert.SerializeObject(actualObject));

                var logs = new List<string>();
                JsonCompareHelper.Compare(logs, JToken.FromObject(expectedObject), JToken.FromObject(actualObject));
                Assert.True(logs.Count == 0, message + Environment.NewLine + string.Join(Environment.NewLine, logs));
            }
        }

        #endregion
    }
}