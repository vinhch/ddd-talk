using System;

using DDDTalk.Business.Shared;
using DDDTalk.Infrastructure.Persistence;

namespace DDDTalk.Business.Products
{
    public class Product : IAggregateRoot, IAuditable
    {
        public int Id { get; set; }
        
        public string CreatedBy { get; set; }
        
        public string CreationDate { get; set; }
        
        public string ModificationBy { get; set; }
        
        public string ModificationDate { get; set; }

        public void ChangePrice(Money newPrice)
        {
        }

        public void ChangeCatalog(string catalogName)
        {
        }

        public void UpdateInformation(string description, string tags, Uri picture)
        {
        }
    }
}