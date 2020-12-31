using BestPractices.Models;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

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

        private Color _vote_color;
        public Color Vote_Color
        {
            get => _vote_color;
            set => Set(ref _vote_color, value);
        }

        public DetailMovieViewModel()
        {

        }
    }
}
