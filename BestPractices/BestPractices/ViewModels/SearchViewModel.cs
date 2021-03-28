using Bestpractices.Service.Interfaces;
using BestPractices.Logging;
using BestPractices.Models;
using BestPractices.Models.Extensions;
using BestPractices.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BestPractices.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public ICommand SearchCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand GoToTrendingCommand { get; set; }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set => SetProperty(ref _searchInput, value);
        }

        public SearchViewModel(IMovieService movieService, ICastService castService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService;
            _castService = castService;
            _logger = loggerAgent;

            SearchCommand = new Command(async () => await Search());
            ClearCommand = new Command(() => { SearchInput = string.Empty; });
            GoToTrendingCommand = new Command(async () => await GoToTrending());
        }

        private async Task GoToTrending()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(page);
            await NavigationService.NavigateToAsync<TrendingMoviesViewModel>();
        }

        private async Task Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchInput))
            {
                //todo build in support for paging
                var movies = await _movieService.SearchMovie(SearchInput, 1);

                var searchResultViewModel = new SearchResultViewModel(_movieService, _castService,_logger)
                {
                    SearchResults = new ObservableCollection<MovieSearch>(movies.ToModel())
                };

                await NavigationService.NavigateToAsync<SearchResultViewModel>(searchResultViewModel);
            }
        }
    }
}
