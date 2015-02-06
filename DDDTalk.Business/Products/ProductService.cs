namespace DDDTalk.Business.Products
{
    /// <remarks>
    /// - Stateless
    /// - For behavior not limited to one Aggregate Root (else use a method on the Aggregate Root)
    /// - Behavior recognized in the domain
    /// - Characteristics
    ///     - Operations performed by Service refer to domain concept which doesn't naturally belong to Entity or Value Object
    ///     - Operation performed refers to other objects in the domain
    ///     - Operation is stateless
    /// </remarks>>
    public class ProductService
    {
    }
}