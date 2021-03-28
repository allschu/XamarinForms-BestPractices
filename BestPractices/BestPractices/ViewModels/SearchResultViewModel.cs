using Bestpractices.Service.Interfaces;
using BestPractices.Globals;
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
    public class SearchResultViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public ICommand ItemClickedCommand { set; get; }

        private ObservableCollection<MovieSearch> _searchResult;
        public ObservableCollection<MovieSearch> SearchResults
        {
            get => _searchResult;
            set => SetProperty(ref _searchResult, value);
        }

        public SearchResultViewModel(IMovieService movieService, ICastService castService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService;
            _castService = castService;
            _logger = loggerAgent;

            ItemClickedCommand = new Command<MovieSearch>(async args => await NavigateToMovieDetails(args));
        }

        private async Task NavigateToMovieDetails(MovieSearch selectedMovie)
        {
            DetailMovieViewModel detailViewModel = null;

            await Task.Run(async () =>
            {
                var movie = await _movieService.GetMovie(selectedMovie.Id).ConfigureAwait(false);
                var cast = await _castService.GetMovieCredits(selectedMovie.Id).ConfigureAwait(false);
                var recommendations = await _movieService.GetMovieRecommendations(selectedMovie.Id).ConfigureAwait(false);

                detailViewModel = new DetailMovieViewModel(_movieService, _castService, _logger)
                {
                    Movie = movie.ToDetailMovie(),
                    CastList = new ObservableCollection<CastList>(cast.ToViewModel()),
                    Recommendations = new ObservableCollection<MovieList>(recommendations.ToMovieList()),
                    DetailTitle = movie.Title,
                    Vote_Color = SharedFunctions.Determine_Vote_Color(movie.Vote_Average)
                };
            }).ContinueWith((args) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavigationService.NavigateToAsync<DetailMovieViewModel>(detailViewModel);
                });
                return Task.CompletedTask;
            }).ConfigureAwait(false);
        }
    }
}
