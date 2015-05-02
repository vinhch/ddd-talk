using System;

namespace DDDTalk.Business.Orders
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void AddProductToOrder(int orderId, int productId, int quantity)
        {
            var order = this.orderRepository.LoadOrder(orderId);

            order.AddOrderLine(productId, quantity);

            this.orderRepository.SaveOrder(order);
        }
    }
}