using DDDTalk.Business.Products;
using DDDTalk.Data.Products;
using DDDTalk.Infrastructure.DependencyInjection;

namespace DDDTalk.Application.Boostrapping
{
    public class DataBootstrapper
    {
        private readonly IContainer container;

        public DataBootstrapper(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            this.container.Register<IProductRepository, ProductRepository>();

            // Initialize other data services...
        }
    }
}