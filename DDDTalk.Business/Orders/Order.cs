using System;
using System.Collections.Generic;
using System.Linq;

using DDDTalk.Infrastructure.Persistence;

namespace DDDTalk.Business.Orders
{
    public class Order : IAggregateRoot, IAuditable
    {
        //private readonly List<OrderLine> orderLines;

        public Order()
        {
            this.OrderLines = new List<OrderLine>();
        }

        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public string CreationDate { get; set; }

        public string ModificationBy { get; set; }

        public string ModificationDate { get; set; }

        public ICollection<OrderLine> OrderLines { get; private set; //get
            //{
            //    return this.orderLines;
            //}
        }

        public void AddOrderLine(int productId, int quantity)
        {
            var orderLine = OrderFactory.CreateOrderLine(productId, quantity);

            this.OrderLines.Add(orderLine);
        }

        public void AddOrderLine(int productId, int quantity, string comment)
        {
            var orderLine = this.OrderLines.SingleOrDefault(ol => ol.ProductId == productId);

            if (orderLine == null)
            {
                orderLine = OrderFactory.CreateOrderLineWithComment(productId, quantity, comment);
                this.OrderLines.Add(orderLine);
            }
            else
            {
                orderLine.AddQuantity(quantity);
                orderLine.ReplaceComment(comment);
            }
        }

        public void RemoveOrderLine(Guid lineId)
        {
            var orderLineToRemove = this.OrderLines.SingleOrDefault(ol => ol.LineId == lineId);

            if (orderLineToRemove == null) throw new ArgumentException("lineId " + lineId + " is not in the Order");

            this.OrderLines.Remove(orderLineToRemove);
        }

        public void EmptyOrder()
        {
            this.OrderLines.Clear();
        }

        public override string ToString()
        {
            var linesInfo = this.OrderLines.Select(ol => 
                string.Format("  LineId: {0}, ProductId: {1}, Quantity: {2}, Comment: {3}", 
                    ol.LineId, ol.ProductId, ol.Quantity, ol.Comment));

            return string.Format("Id: {0}{1}{2}", 
                this.Id, Environment.NewLine, string.Join(Environment.NewLine, linesInfo));
        }
    }
}
