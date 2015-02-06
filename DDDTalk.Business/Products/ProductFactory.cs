using DDDTalk.Business.Shared;

namespace DDDTalk.Business.Products
{
    /// <remarks>
    /// Factory
    /// - For creation and initialization of complex object structures
    /// - Respect invariants
    /// - Creation as an atomic unit (complete or fail, no partial)
    /// - Creates entire Aggregates
    /// - Can use simple constructor on Aggregate Root Entity if construction is simple enough
    /// </remarks>
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