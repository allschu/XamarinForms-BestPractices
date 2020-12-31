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
        private readonly ILoggerAgent _logger;

        public RelayCommand SearchCommand { get; set; }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set => Set(ref _searchInput, value);
        }

        public SearchViewModel(IMovieService movieService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService;
            _logger = loggerAgent;

            SearchCommand = new RelayCommand(async () => await Search());
        }

        private async Task Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchInput))
            {
                //todo build in support for paging
                var movies = await _movieService.SearchMovie(SearchInput, 1);

                var searchResultViewModel = new SearchResultViewModel
                {
                    SearchResults = new ObservableCollection<MovieSearch>(movies.ToModel())
                };

                var page = new SearchResultPage();
                page.BindingContext = searchResultViewModel;

                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
        }
    }
}
