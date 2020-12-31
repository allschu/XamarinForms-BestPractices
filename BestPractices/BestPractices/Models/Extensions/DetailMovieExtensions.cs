using BestPractices.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestPractices.Models.Extensions
{
    internal static class DetailMovieExtensions
    {
        public static MovieDetailPageModel ToDetailMovie(this MovieDetail movie)
        {
            return new MovieDetailPageModel(
                movie.Id,
                movie.Overview,
                movie.Release_date,
                movie.Status,
                movie.Tagline,
                movie.Title,
                Math.Round(movie.Vote_Average, 1),
                movie.Vote_Count,
                movie.Poster_path
                );
        }
    }
}
