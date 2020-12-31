using BestPractices.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BestPractices.ViewModels
{
    public class SearchResultViewModel : ViewModelBase
    {
        private ObservableCollection<MovieSearch> _searchResult;
        public ObservableCollection<MovieSearch> SearchResults
        {
            get => _searchResult;
            set => Set(ref _searchResult, value);
        }

        public SearchResultViewModel()
        {

        }
    }
}
