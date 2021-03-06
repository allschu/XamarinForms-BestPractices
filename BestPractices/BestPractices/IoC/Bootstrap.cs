using Bestpractices.Service;
using Bestpractices.Service.Interfaces;
using BestPractices.Common;
using BestPractices.Logging;
using BestPractices.Services;
using BestPractices.Services.Interfaces;
using Nancy.TinyIoc;

namespace BestPractices.IoC
{
    public static class Bootstrap
    {
        public static ILocator GetLocater()
        {
            return new Locator(GetContainer());
        }

        internal static TinyIoCContainer GetContainer()
        {
            //add dependencies here
            var container = new TinyIoCContainer();
            container.Register<ILoggerAgent, LoggerAgent>().AsSingleton();
            container.Register<IMovieService, MovieService>();
            container.Register<ICastService, CastService>();
            container.Register<INavigationService, NavigationService>();

            return container;
        }
    }
}
