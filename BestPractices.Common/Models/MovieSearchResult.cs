using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Common.Models
{
    public class MovieSearchResult
    {
        public int Id { get; }
        public string Title { get;  }
        public string ImagePath { get; }
        public DateTime? ReleaseDate { get; }

        public MovieSearchResult(int id, string title, string imagePath, DateTime? release_date)
        {
            Id = id;
            Title = title;
            ImagePath = imagePath;
            ReleaseDate = release_date;
        }
    }
}
