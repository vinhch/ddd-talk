namespace DDDTalk.Infrastructure.DependencyInjection
{
    public class ContainerFactory
    {
        public static IContainer Create()
        {
            return new DependencyInjectionContainer();
        }
    }
}