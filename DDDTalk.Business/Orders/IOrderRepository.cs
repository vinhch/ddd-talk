namespace DDDTalk.Business.Orders
{
    public interface IOrderRepository
    {
        Order LoadOrder(int orderId);

        void SaveOrder(Order order);
    }
}