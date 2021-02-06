using System.Collections.Generic;
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
    public class TrendingMoviesViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public RelayCommand<MovieList> ItemClickedCommand { set; get; }

        private ObservableCollection<MovieList> _movieList;
        public ObservableCollection<MovieList> MovieList
        {
            get => _movieList;
            set => Set(ref _movieList, value);
        }

        public TrendingMoviesViewModel(IMovieService movieService, ICastService castService, ILoggerAgent logger)
        {
            _movieService = movieService;
            _castService = castService;
            _logger = logger;

            ItemClickedCommand = new RelayCommand<MovieList>(async args => await NavigateToMovieDetails(args));

            Task.Run(async () => await LoadView()); ;
        }

        private async Task LoadView()
        {
            _logger.Information("Screen loads");

            var movies = await _movieService.GetTrendingMovies();

            MovieList = new ObservableCollection<MovieList>(movies.ToModel());
        }

        private async Task NavigateToMovieDetails(MovieList selectedMovie)
        {
            if (selectedMovie != null)
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
}
