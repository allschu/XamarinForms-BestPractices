using BestPractices.Common;
using BestPractices.IoC;
using BestPractices.ViewModels;

namespace BestPractices
{
    public static class ViewModelLocator
    {
        private static readonly ILocator Locator = Bootstrap.GetLocater();

        static ViewModelLocator()
        {
            Locator.Register<TrendingMoviesViewModel>();
            Locator.Register<DetailMovieViewModel>();
            Locator.Register<SearchViewModel>();
            Locator.Register<SearchResultViewModel>();
        }

        public static T Resolve<T>() where T : class
        {
            return Locator.GetInstance<T>();
        }

        public static TrendingMoviesViewModel TrendingMoviesViewModel => Locator.GetInstance<TrendingMoviesViewModel>();
        public static DetailMovieViewModel DetailMovieViewModel => Locator.GetInstance<DetailMovieViewModel>();
        public static SearchViewModel SearchViewModel => Locator.GetInstance<SearchViewModel>();
        public static SearchResultViewModel SearchResultViewModel => Locator.GetInstance<SearchResultViewModel>();
    }
}
