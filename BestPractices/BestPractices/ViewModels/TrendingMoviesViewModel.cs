using Bestpractices.Service.Interfaces;
using BestPractices.Globals;
using BestPractices.Logging;
using BestPractices.Models;
using BestPractices.Models.Extensions;
using BestPractices.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BestPractices.ViewModels
{
    public class TrendingMoviesViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public ICommand ItemClickedCommand { set; get; }

        private ObservableCollection<MovieList> _movieList;
        public ObservableCollection<MovieList> MovieList
        {
            get => _movieList;
            set => SetProperty(ref _movieList, value);
        }

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            set => SetProperty(ref _loading, value);
        }

        public TrendingMoviesViewModel(IMovieService movieService, ICastService castService, ILoggerAgent logger)
        {
            Loading = false;
            _movieService = movieService;
            _castService = castService;
            _logger = logger;

            ItemClickedCommand = new Command<MovieList>(async args => await NavigateToMovieDetails(args));

            Task.Run(async () => await LoadView()); ;
        }

        private async Task LoadView()
        {
            Loading = true;

            _logger.Information("Screen loads");

            var movies = await _movieService.GetTrendingMovies();

            MovieList = new ObservableCollection<MovieList>(movies.ToModel());

            Loading = false;
        }

        private async Task NavigateToMovieDetails(MovieList selectedMovie)
        {
            if (selectedMovie == null)
                throw new ArgumentNullException();

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
