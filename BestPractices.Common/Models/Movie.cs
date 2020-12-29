using System;

namespace BestPractices.Common.Models
{
    public class Movie
    {
        public int Id { get; }
        public string Tagline { get; }
        public string Title { get; }
        public string Image_path { get; }
        public DateTime Release_Date { get; }

        public Movie(int id, string title, string tagline, string image, DateTime release_date)
        {
            Id = id;
            Tagline = tagline;
            Title = title;
            Image_path = image;
            Release_Date = release_date;
        }
    }
}
