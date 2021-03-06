using BestPractices.Common;
using Nancy.TinyIoc;
using System;

namespace BestPractices.IoC
{
    public class Locator : ILocator
    {
        private readonly TinyIoCContainer _container;

        public Locator(TinyIoCContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public T GetInstance<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public void Register<T>() where T : class
        {
            _container.Register<T>();
        }
    }
}
