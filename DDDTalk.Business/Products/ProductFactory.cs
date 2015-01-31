using DDDTalk.Business.Shared;

namespace DDDTalk.Business.Products
{
    public class ProductFactory
    {
        public Product CreatePhysicalProduct(string name, Weight weight, Dimensions dimensions)
        {
            return new Product();
        }

        public Product CreateDigitalProduct(string name, StorageSize storageSize)
        {
            return new Product();
        }
    }
}