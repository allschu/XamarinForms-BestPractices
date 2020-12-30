using Bestpractices.Service.Interfaces;
using BestPractices.Logging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                var movies = _movieService.SearchMovie(SearchInput, 1);
            }
        }
    }
}
