﻿using Bestpractices.Service.Contract;
using BestPractices.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bestpractices.Service.Extensions
{
    internal static class MovieExtensions
    {
        public static MovieSearchResult ToMovieSearchResult(this MovieSearchResultDTO movie)
        {
            return new MovieSearchResult(movie.id, movie.Title, Constants.POSTPATH_PREFIX + movie.poster_path, movie.release_date);
        }

        public static IEnumerable<MovieSearchResult> ToMovieSearchResultList(this MovieSearchResultListDTO movieSearchResultList)
        {
            return movieSearchResultList.results.Select(x => x.ToMovieSearchResult());
        }

        public static MovieDetail ToMovieDetail(this MovieDetailDTO movieDetailDTO)
        {
            return new MovieDetail(
                movieDetailDTO.Id,
                movieDetailDTO.Overview,
                movieDetailDTO.Release_date,
                movieDetailDTO.Status,
                movieDetailDTO.Tagline,
                movieDetailDTO.Title,
                movieDetailDTO.Vote_Average,
                movieDetailDTO.Vote_Count,
                Constants.POSTPATH_PREFIX + movieDetailDTO.Poster_path
                );
        }

        public static Movie ToMovie(this MovieDetailDTO movieDetailDTO)
        {
            return new Movie(
                movieDetailDTO.Id,
                movieDetailDTO.Title,
                movieDetailDTO.Tagline,
                Constants.POSTPATH_PREFIX + movieDetailDTO.Poster_path,
                movieDetailDTO.Release_date
                );
        }

        public static IEnumerable<Movie> ToMovieList(this MovieDetailResultListDTO movieDetailResultListDTO)
        {
            return movieDetailResultListDTO.results.Select(movie => movie.ToMovie());
        }
    }
}
