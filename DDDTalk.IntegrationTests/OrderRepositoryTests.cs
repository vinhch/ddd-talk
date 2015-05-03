using System;
using System.Linq;

using DDDTalk.Data.Orders;

using Xunit;

namespace DDDTalk.IntegrationTests
{
    public class OrderRepositoryTests
    {
        private readonly OrderRepository orderRepository;

        public OrderRepositoryTests()
        {
            this.orderRepository = new OrderRepository();
        }

        [Fact]
        public void SaveOrder_WithExistingOrder_ActuallyWorks()
        {
            var currentTime = DateTime.Now.ToShortTimeString();

            // Arrange
            var order = this.orderRepository.LoadOrder(12);
            order.OrderLines.ElementAt(0).Comment = currentTime;

            // Act
            this.orderRepository.SaveOrder(order);

            // Assert
            var actualOrder = this.orderRepository.LoadOrder(12);
            Assert.Equal(currentTime, actualOrder.OrderLines.ElementAt(0).Comment);
        }
    }
}
