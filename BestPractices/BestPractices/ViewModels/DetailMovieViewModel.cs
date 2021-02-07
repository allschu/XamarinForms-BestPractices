using Bestpractices.Service.Interfaces;
using BestPractices.Globals;
using BestPractices.Logging;
using BestPractices.Models;
using BestPractices.Models.Extensions;
using BestPractices.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BestPractices.ViewModels
{
    public class DetailMovieViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public RelayCommand<MovieList> ItemClickedCommand { set; get; }

        private MovieDetailPageModel _movie;
        public MovieDetailPageModel Movie
        {
            get => _movie;
            set => Set(ref _movie, value);
        }

        private ObservableCollection<CastList> _castList;
        public ObservableCollection<CastList> CastList
        {
            get => _castList;
            set => Set(ref _castList, value);
        }
        
        private ObservableCollection<MovieList> _recommandations;
        public ObservableCollection<MovieList> Recommendations
        {
            get => _recommandations;
            set => Set(ref _recommandations, value);
        }

        private string _detailTitle;
        public string DetailTitle
        {
            get => _detailTitle;
            set => Set(ref _detailTitle, value);
        }

        private Color _vote_color;
        public Color Vote_Color
        {
            get => _vote_color;
            set => Set(ref _vote_color, value);
        }

        public DetailMovieViewModel(IMovieService movieService, ICastService castService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _castService = castService ?? throw new ArgumentNullException(nameof(castService));
            _logger = loggerAgent ?? throw new ArgumentNullException(nameof(loggerAgent));

            ItemClickedCommand = new RelayCommand<MovieList>(async args => await NavigateToMovieDetails(args));
        }

        private async Task NavigateToMovieDetails(MovieList selectedMovie)
        {
            if (selectedMovie == null)
                throw new ArgumentNullException(nameof(selectedMovie));

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
