namespace DDDTalk.Infrastructure.DependencyInjection
{
    public interface IContainer
    {
        void Register<T>();

        void Register<TInterface, TImplementation>();

        T Resolve<T>();
    }
}