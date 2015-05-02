using System;

namespace DDDTalk.Business.Orders
{
    public class OrderLine
    {
        public Guid LineId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string Comment { get; set; }
    }
}