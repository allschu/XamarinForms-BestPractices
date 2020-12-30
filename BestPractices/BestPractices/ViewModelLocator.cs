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
            Locator.Register<DetailMovieViewModel>();
            Locator.Register<SearchViewModel>();
        }

        public static TrendingMoviesViewModel TrendingMoviesViewModel => Locator.GetInstance<TrendingMoviesViewModel>();
        public static DetailMovieViewModel DetailMovieViewModel => Locator.GetInstance<DetailMovieViewModel>();
        public static SearchViewModel SearchViewModel => Locator.GetInstance<SearchViewModel>();
    }
}
