using System;

namespace BestPractices.Models
{
    public class MovieList
    {
        public int Id { get; }
        public string Title { get; }
        public string ImagePath { get; }
        public string Release_Date { get; }

        public MovieList(int id, string title, string imagePath, string release_date)
        {
            Id = id;
            Title = title;
            ImagePath = imagePath;
            Release_Date = release_date;
        }
    }
}
