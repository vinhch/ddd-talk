using System;
using System.Collections.Generic;

using DDDTalk.Business.Shared;

namespace DDDTalk.Business.Products
{
    /// <remarks>
    /// Repository
    /// - Encapsulate all the logic needed to obtain existing object references
    /// - May store references to some objects
    /// - May use a Strategy to access persistence storage or another
    /// - Provide methods to add and remove objects
    /// - Provide methods that select objects based on some criteria
    /// - Only for Aggregate roots
    /// - Should not be mixed with Factories
    /// ! Please don't use generic Repositories
    /// </remarks>
    public interface IProductRepository
    {
        Product LoadProductAggregateById(int id);

        void AddNewProduct(Product product);

        IEnumerable<Product> QueryProductsByCatalog(string catalogName);

        IEnumerable<Product> QueryArchivedProducts(string catalogName);

        IEnumerable<Product> QueryDiscountedProducts(string nameFilter, DateTime? discountExpirationDateFilter, Money maxPriceFilter);
    }
}