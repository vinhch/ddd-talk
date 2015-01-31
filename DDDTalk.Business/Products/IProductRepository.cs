using System;
using System.Collections.Generic;

using DDDTalk.Business.Shared;

namespace DDDTalk.Business.Products
{
    public interface IProductRepository
    {
        Product LoadProductAggregateById(int id);

        void AddNewProduct(Product product);

        IEnumerable<Product> QueryProductsByCatalog(string catalogName);

        IEnumerable<Product> QueryArchivedProducts(string catalogName);

        IEnumerable<Product> QueryDiscountedProducts(string nameFilter, DateTime? discountExpirationDateFilter, Money maxPriceFilter);
    }
}