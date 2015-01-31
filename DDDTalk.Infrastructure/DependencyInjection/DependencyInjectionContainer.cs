using System;
using System.Collections.Generic;

namespace DDDTalk.Infrastructure.DependencyInjection
{
    public class DependencyInjectionContainer : IContainer
    {
        private readonly Dictionary<Type, Type> registry;

        public DependencyInjectionContainer()
        {
            this.registry = new Dictionary<Type, Type>();
        }

        public void Register<T>()
        {
            this.registry.Add(typeof(T), typeof(T));
        }

        public void Register<TInterface, TImplementation>()
        {
            this.registry.Add(typeof(TInterface), typeof(TImplementation));
        }

        public T Resolve<T>()
        {
            var implementationType = this.registry[typeof(T)];

            return (T)Activator.CreateInstance(implementationType);
        }
    }
}