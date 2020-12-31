using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Models
{
    public class MovieSearch
    {
        public int Id { get; }
        public string Title { get; }
        public string ReleaseDate { get; }
        public string ImagePath { get; }

        public MovieSearch(int id, string title, string imagePath, string release_date)
        {
            Id = id;
            Title = title;
            ReleaseDate = release_date;
            ImagePath = imagePath;
        }
    }
}
