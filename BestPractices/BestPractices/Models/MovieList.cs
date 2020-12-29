using System;

namespace BestPractices.Models
{
    public class MovieList
    {
        public int Id { get; }
        public string Title { get; }
        public string Intro { get; }
        public string ImagePath { get; }
        public DateTime Release_Date { get; }

        public MovieList(int id, string title, string intro, string imagePath, DateTime release_date)
        {
            Id = id;
            Title = title;
            Intro = intro;
            ImagePath = imagePath;
            Release_Date = release_date;
        }
    }
}
