using System;

namespace DDDTalk.Business.Orders
{
    public class OrderLine
    {
        public Guid LineId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string Comment { get; set; }

        public void AddQuantity(int quantity)
        {
            if (quantity < 1) throw new ArgumentException("Quantity must be greater than 1");

            this.Quantity += quantity;
        }

        public void ReplaceComment(string comment)
        {
            if (comment == null) throw new ArgumentNullException("comment");
            if (comment.Length < 3) throw new ArgumentException("Comment is too short");
            if (comment.Length > 40) throw new ArgumentException("Comment is too long");

            this.Comment = comment;
        }
    }
}