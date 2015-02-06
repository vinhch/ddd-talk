using System;

using DDDTalk.Business.Shared;
using DDDTalk.Infrastructure.Persistence;

namespace DDDTalk.Business.Products
{
    /// <remark>
    /// Entity
    /// - Has an identity (two Entities with the same identities are equals)
    /// Aggregate Root
    /// - Boundary around objects inside (can't access objects directly, must go throught the Root)
    /// - Enforce invariants of the Entities inside
    /// - Root Entity has global identity
    /// - Internal Entities have local identity
    /// - Contains no direct references to other Aggregate, only IDs
    /// </remark>
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