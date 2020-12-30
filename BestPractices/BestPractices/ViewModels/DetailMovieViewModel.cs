using BestPractices.Models;
using GalaSoft.MvvmLight;

namespace BestPractices.ViewModels
{
    public class DetailMovieViewModel : ViewModelBase
    {
        private MovieDetailPageModel _movie;
        public MovieDetailPageModel Movie
        {
            get => _movie;
            set => Set(ref _movie, value);
        }

        private string _detailTitle;
        public string DetailTitle
        {
            get => _detailTitle;
            set => Set(ref _detailTitle, value);
        }

        public DetailMovieViewModel()
        {

        }
    }
}
