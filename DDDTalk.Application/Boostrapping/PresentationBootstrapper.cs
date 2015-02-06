using DDDTalk.Infrastructure.DependencyInjection;
using DDDTalk.Infrastructure.Navigation;
using DDDTalk.Presentation;
using DDDTalk.Presentation.Products;

namespace DDDTalk.Application.Boostrapping
{
    public class PresentationBootstrapper
    {
        private readonly IContainer container;

        public PresentationBootstrapper(IContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            // Register Views
            this.container.Register<ProductInfoView>();

            // Register ViewModels
            this.container.Register<ProductInfoViewModel>();

            // Register the main window
            this.container.Register<IMainWindow, Shell>();

            // Initialize other presentation services...
        }
    }
}