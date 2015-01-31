using System;

using DDDTalk.Infrastructure.DependencyInjection;
using DDDTalk.Infrastructure.Navigation;

namespace DDDTalk.Application.Boostrapping
{
    public class ApplicationBootstrapper
    {
        private IContainer container;

        public void Initialize()
        {
            var infrastructureBootstrapper = new InfrastructureBootstrapper();
            infrastructureBootstrapper.Initialize();

            this.container = infrastructureBootstrapper.Container;

            var dataBootstrapper = new DataBootstrapper(this.container);
            dataBootstrapper.Initialize();

            var presentationBootstrapper = new PresentationBootstrapper(this.container);
            presentationBootstrapper.Initialize();

            // Initialize other application services
        }

        public IMainWindow CreateMainWindow()
        {
            return this.container.Resolve<IMainWindow>();
        }
    }
}
