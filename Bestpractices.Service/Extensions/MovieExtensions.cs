using Bestpractices.Service.Contract;
using BestPractices.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bestpractices.Service.Extensions
{
    internal static class MovieExtensions
    {
        public static MovieSearchResult ToMovieSearchResult(this MovieSearchResultDTO movie)
        {
            return new MovieSearchResult(movie.id, movie.Title, Constants.POSTER_PATH_PREFIX + movie.poster_path, movie.release_date);
        }

        public static IEnumerable<MovieSearchResult> ToMovieSearchResultList(this MovieSearchResultListDTO movieSearchResultList)
        {
            return movieSearchResultList.results.Select(x => x.ToMovieSearchResult());
        }

        public static MovieDetail ToMovieDetail(this MovieDetailDTO movieDetailDto)
        {
            return new MovieDetail(
                movieDetailDto.Id,
                movieDetailDto.Overview,
                movieDetailDto.Release_date,
                movieDetailDto.Status,
                movieDetailDto.Tagline,
                movieDetailDto.Title,
                movieDetailDto.Vote_Average,
                movieDetailDto.Vote_Count,
                Constants.POSTER_PATH_PREFIX + movieDetailDto.Poster_path
                );
        }

        public static Movie ToMovie(this MovieDetailDTO movieDetailDto)
        {
            return new Movie(
                movieDetailDto.Id,
                movieDetailDto.Title,
                movieDetailDto.Tagline,
                Constants.POSTER_PATH_PREFIX + movieDetailDto.Poster_path,
                movieDetailDto.Release_date
                );
        }

        public static IEnumerable<Movie> ToMovieList(this MovieDetailResultListDTO movieDetailResultListDto)
        {
            return movieDetailResultListDto.results.Select(movie => movie.ToMovie());
        }
    }
}
