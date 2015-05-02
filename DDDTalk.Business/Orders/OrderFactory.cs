using System;

namespace DDDTalk.Business.Orders
{
    internal static class OrderFactory
    {
        public static OrderLine CreateOrderLine(int productId, int quantity)
        {
            if (productId < 0) throw new ArgumentException("ProductId must be greater than 0");
            if (quantity < 1) throw new ArgumentException("Quantity must be greater than 1");

            return new OrderLine
            {
                LineId = Guid.NewGuid(),
                ProductId = productId,
                Quantity = quantity
            };
        }

        public static OrderLine CreateOrderLineWithComment(int productId, int quantity, string comment)
        {
            if (comment == null) throw new ArgumentNullException("comment");
            if (comment.Length < 3) throw new ArgumentException("Comment is too short");
            if (comment.Length > 40) throw new ArgumentException("Comment is too long");

            var orderLine = CreateOrderLine(productId, quantity);
            orderLine.Comment = comment;

            return orderLine;
        }
    }
}