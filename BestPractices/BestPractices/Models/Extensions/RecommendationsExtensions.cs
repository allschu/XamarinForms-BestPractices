using BestPractices.Common.Models;
using BestPractices.Globals;
using System.Collections.Generic;
using System.Linq;

namespace BestPractices.Models.Extensions
{
    internal static class RecommendationsExtensions
    {

        public static MovieList ToMovieList(this MovieSearchResult movie)
        {
            return new MovieList(movie.Id, movie.Title, movie.ImagePath, SharedFunctions.GetCultureDate(movie.ReleaseDate));
        }

        public static List<MovieList> ToMovieList(this IEnumerable<MovieSearchResult> movies)
        {
            return movies.Select(x => x.ToMovieList()).ToList();
        }
    }
}
