using System;
using System.Collections.Generic;

using DDDTalk.Business.Products;
using DDDTalk.Business.Shared;

namespace DDDTalk.Data.Products
{
    public class ProductRepository : IProductRepository
    {
        public Product LoadProductAggregateById(int id)
        {
            return null;
        }

        public void AddNewProduct(Product product)
        {
        }

        public IEnumerable<Product> QueryProductsByCatalog(string catalogName)
        {
            yield break;
        }

        public IEnumerable<Product> QueryArchivedProducts(string catalogName)
        {
            yield break;
        }

        public IEnumerable<Product> QueryDiscountedProducts(string nameFilter, DateTime? discountExpirationDateFilter, Money maxPriceFilter)
        {
            yield break;
        }
    }
}