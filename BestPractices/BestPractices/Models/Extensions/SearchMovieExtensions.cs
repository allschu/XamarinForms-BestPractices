using BestPractices.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BestPractices.Models.Extensions
{
    internal static class SearchMovieExtensions
    {
        public static MovieSearch ToModel(this MovieSearchResult movie)
        {
            return new MovieSearch(movie.Id, movie.Title, movie.ImagePath, GetCultureDate(movie.ReleaseDate));
        }

        public static List<MovieSearch> ToModel(this IEnumerable<MovieSearchResult> movies)
        {
            return movies.Select(x => x.ToModel()).ToList();
        }

        private static string GetCultureDate(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToShortDateString();
            }
            return string.Empty;
        }
    }
}
