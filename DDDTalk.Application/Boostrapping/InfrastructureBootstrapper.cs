using DDDTalk.Infrastructure.DependencyInjection;

namespace DDDTalk.Application.Boostrapping
{
    public class InfrastructureBootstrapper
    {
        public IContainer Container { get; private set; }

        public void Initialize()
        {
            this.Container = ContainerFactory.Create();

            // Initialize other infrastructure services...
        }
    }
}