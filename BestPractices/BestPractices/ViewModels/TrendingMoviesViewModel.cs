using Bestpractices.Service.Interfaces;
using BestPractices.Logging;
using BestPractices.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace BestPractices.ViewModels
{
    public class TrendingMoviesViewModel : ViewModelBase
    {
        private readonly IMovieService _movieService;
        private readonly ILoggerAgent _logger;

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

            Task.Run(async () => await LoadView()); ;
        }

        private async Task LoadView()
        {
            _logger.Information("Screen loads");

            var movies = await _movieService.GetTrendingMovies();
        }
    }
}
