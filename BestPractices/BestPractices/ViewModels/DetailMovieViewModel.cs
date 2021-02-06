﻿using Bestpractices.Service.Interfaces;
using BestPractices.Logging;
using BestPractices.Models;
using BestPractices.Models.Extensions;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BestPractices.Common.Models;
using Xamarin.Forms;

namespace BestPractices.ViewModels
{
    public class DetailMovieViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ILoggerAgent _logger;

        private MovieDetailPageModel _movie;
        public MovieDetailPageModel Movie
        {
            get => _movie;
            set => Set(ref _movie, value);
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

        public DetailMovieViewModel(IMovieService movieService, ILoggerAgent loggerAgent)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _logger = loggerAgent ?? throw new ArgumentNullException(nameof(loggerAgent));
        }

        private async Task LoadView()
        {
            if (Movie?.Id > 0)
            {
                var recommendations = await _movieService.GetMovieRecommendations(Movie.Id);

                var movieSearchResults = recommendations as MovieSearchResult[] ?? recommendations.ToArray();
                if (movieSearchResults.Any())
                {
                    Recommendations = new ObservableCollection<MovieList>(movieSearchResults.ToMovieList());
                }
                else
                {
                    _logger.Information($"No recommendations present for movie {Movie.Id}");
                }
            }
            else
            {
                _logger.Warning("Cannot load recommendations, missing movie id");
            }
        }
    }
}
