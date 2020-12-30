using System;

namespace BestPractices.Models
{
    public class MovieDetailPageModel
    {
        public int Id { get; }
        public string Overview { get; }
        public DateTime Release_date { get; }
        public string Status { get; }
        public string Tagline { get; }
        public string Title { get; }
        public double Vote_Average { get; }
        public int Vote_Count { get; }
        public string Poster_path { get; }

        public MovieDetailPageModel(int id, string overview, DateTime release_date, string status, string tagline, string title, double vote_average, int vote_count, string posterPath)
        {
            Id = id;
            Overview = overview;
            Release_date = release_date;
            Status = status;
            Tagline = tagline;
            Title = title;
            Vote_Average = vote_average;
            Vote_Count = vote_count;
            Poster_path = posterPath;
        }
    }
}
