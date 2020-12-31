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
        private readonly ILoggerAgent _logger;

        public RelayCommand<MovieList> ItemClickedCommand { set; get; }

        private ObservableCollection<MovieList> _movieList;
        public ObservableCollection<MovieList> MovieList
        {
            get => _movieList;
            set => Set(ref _movieList, value);
        }

        public TrendingMoviesViewModel(IMovieService movieService, ILoggerAgent logger)
        {
            _movieService = movieService;
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
                var movie = await _movieService.GetMovie(selectedMovie.Id);

                var detailViewModel = new DetailMovieViewModel
                {
                    Movie = movie.ToDetailMovie(),
                    DetailTitle = movie.Title,
                    Vote_Color = SharedFunctions.Determine_Vote_Color(movie.Vote_Average)
                };

                var page = new DetailMoviePage
                {
                    BindingContext = detailViewModel
                };

                await Application.Current.MainPage.Navigation.PushAsync(page);
            } 
        }
    }
}
