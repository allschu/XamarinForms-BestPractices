﻿using Bestpractices.Service.Interfaces;
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
    public class DetailMovieViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ICastService _castService;
        private readonly ILoggerAgent _logger;

        public ICommand ItemClickedCommand { set; get; }

        private MovieDetailPageModel _movie;
        public MovieDetailPageModel Movie
        {
            get => _movie;
            set => SetProperty(ref _movie, value);
        }

        private ObservableCollection<CastList> _castList;
        public ObservableCollection<CastList> CastList
        {
            get => _castList;
            set => SetProperty(ref _castList, value);
        }
        
        private ObservableCollection<MovieList> _recommandations;
        public ObservableCollection<MovieList> Recommendations
        {
            get => _recommandations;
            set => SetProperty(ref _recommandations, value);
        }

        private string _detailTitle;
        public string DetailTitle
        {
            get => _detailTitle;
            set => SetProperty(ref _detailTitle, value);
        }

        private Color _vote_color;
        public Color Vote_Color
        {
            get => _vote_color;
            set => SetProperty(ref _vote_color, value);
        }

        public DetailMovieViewModel(IMovieService movieService, ICastService castService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _castService = castService ?? throw new ArgumentNullException(nameof(castService));
            _logger = loggerAgent ?? throw new ArgumentNullException(nameof(loggerAgent));

            ItemClickedCommand = new Command<MovieList>(async args => await NavigateToMovieDetails(args));
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
                    await NavigationService.NavigateToAsync<DetailMovieViewModel>(detailViewModel);
                });
                return Task.CompletedTask;
            }).ConfigureAwait(false);
        }
    }
}
