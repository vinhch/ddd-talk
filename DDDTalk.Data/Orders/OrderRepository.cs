using System;
using System.Collections.Generic;
using System.Linq;

using DDDTalk.Business.Orders;

namespace DDDTalk.Data.Orders
{
    /// <summary>
    /// Not a real Repository, only in memory for demo purposes
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> cache;

        public OrderRepository()
        {
            this.cache = new List<Order>();
        }

        public Order LoadOrder(int orderId)
        {
            var order = this.cache.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                order = new Order { Id = orderId };
                order.AddOrderLine(1, 1);
            }

            return order;
        }

        public void SaveOrder(Order order)
        {
            if (this.cache.All(o => o.Id != order.Id))
            {
                this.cache.Add(order);
            }
        }
    }
}
