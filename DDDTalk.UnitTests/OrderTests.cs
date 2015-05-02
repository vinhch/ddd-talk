using System;
using System.Linq;

using DDDTalk.Business.Orders;

using Xunit;

namespace DDDTalk.UnitTests
{
    public class OrderTests
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var order = new Order();

            // Act
            order.AddOrderLine(12, 34, "backorder");

            // Assert
            Assert.Equal(1, order.OrderLines.Count());
        }
    }
}
