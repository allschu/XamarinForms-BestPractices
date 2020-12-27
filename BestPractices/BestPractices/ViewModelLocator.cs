using BestPractices.Common;
using BestPractices.Dependency;
using BestPractices.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices
{
    public static class ViewModelLocator
    {
        private static ILocator Locator = Bootstrap.GetLocater();

        static ViewModelLocator()
        {
            Locator.Register<TrendingMoviesViewModel>();
        }

        public static TrendingMoviesViewModel TrendingMoviesViewModel => Locator.GetInstance<TrendingMoviesViewModel>();
    }
}
