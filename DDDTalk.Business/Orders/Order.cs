using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using DDDTalk.Infrastructure.Persistence;

namespace DDDTalk.Business.Orders
{
    public class Order : IAggregateRoot, IAuditable
    {
        private List<OrderLine> orderLines;

        public Order()
        {
            this.orderLines = new List<OrderLine>();
        }

        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public string CreationDate { get; set; }

        public string ModificationBy { get; set; }

        public string ModificationDate { get; set; }

        public IEnumerable<OrderLine> OrderLines
        {
            get
            {
                return this.orderLines;
            }
        }

        public void AddOrderLine(int productId, int quantity, string comment)
        {
            var orderLine = OrderFactory.CreateOrderLineWithComment(productId, quantity, comment);

            this.orderLines.Add(orderLine);
        }

        public void RemoveOrderLine(Guid lineId)
        {
            var orderLineToRemove = this.orderLines.SingleOrDefault(ol => ol.LineId == lineId);

            if (orderLineToRemove == null) throw new ArgumentException("lineId " + lineId + " is not in the Order");

            this.orderLines.Remove(orderLineToRemove);
        }

        public void EmptyOrder()
        {
            this.orderLines.Clear();
        }
    }
}
