using Bestpractices.Service.Interfaces;
using BestPractices.Globals;
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
    public class SearchResultViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public RelayCommand<MovieSearch> ItemClickedCommand { set; get; }

        private ObservableCollection<MovieSearch> _searchResult;
        public ObservableCollection<MovieSearch> SearchResults
        {
            get => _searchResult;
            set => Set(ref _searchResult, value);
        }

        public SearchResultViewModel(IMovieService movieService, ICastService castService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService;
            _castService = castService;
            _logger = loggerAgent;

            ItemClickedCommand = new RelayCommand<MovieSearch>(async args => await NavigateToMovieDetails(args));
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
                    var page = new DetailMoviePage
                    {
                        BindingContext = detailViewModel
                    };

                    await Application.Current.MainPage.Navigation.PushAsync(page);
                });
                return Task.CompletedTask;
            }).ConfigureAwait(false);
        }
    }
}
