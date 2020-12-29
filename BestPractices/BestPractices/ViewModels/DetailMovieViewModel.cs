using BestPractices.Models;
using GalaSoft.MvvmLight;

namespace BestPractices.ViewModels
{
    public class DetailMovieViewModel : ViewModelBase
    {
        private MovieDetail _movie;
        public MovieDetail Movie
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
