using Bestpractices.Service;
using Bestpractices.Service.Interfaces;
using BestPractices.Common;
using BestPractices.Logging;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Dependency
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
            container.Register<ILoggerAgent, LoggerAgent>();
            container.Register<IMovieService, MovieService>();

            return container;
        }
    }
}
