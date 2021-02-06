using Bestpractices.Service.Interfaces;
using BestPractices.Logging;
using BestPractices.Models;
using BestPractices.Models.Extensions;
using BestPractices.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BestPractices.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }
        public RelayCommand GoToTrendingCommand { get; set; }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set => Set(ref _searchInput, value);
        }

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => Set(ref _loading, value);
        }

        public SearchViewModel(IMovieService movieService, ICastService castService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService;
            _castService = castService;
            _logger = loggerAgent;

            Loading = false;
            SearchCommand = new RelayCommand(async () => await Search());
            ClearCommand = new RelayCommand(() => { SearchInput = string.Empty; });
            GoToTrendingCommand = new RelayCommand(async () => await GoToTrending());
        }

        private async Task GoToTrending()
        {
            Loading = true;
            var movies = await _movieService.GetTrendingMovies();

            var trendingViewModel = new TrendingMoviesViewModel(_movieService, _castService,_logger)
            {
                MovieList = new ObservableCollection<MovieList>(movies.ToModel())
            };
            var page = new TrendingMoviesPage
            {
                BindingContext = trendingViewModel
            };
            Loading = false;
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private async Task Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchInput))
            {
                Loading = true;

                //todo build in support for paging
                var movies = await _movieService.SearchMovie(SearchInput, 1);

                var searchResultViewModel = new SearchResultViewModel(_movieService, _castService,_logger)
                {
                    SearchResults = new ObservableCollection<MovieSearch>(movies.ToModel())
                };

                var page = new SearchResultPage
                {
                    BindingContext = searchResultViewModel
                };

                Loading = false;
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
        }
    }
}
