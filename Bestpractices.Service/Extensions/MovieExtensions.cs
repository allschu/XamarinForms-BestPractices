using Bestpractices.Service.Contract;
using BestPractices.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bestpractices.Service.Extensions
{
    internal static class MovieExtensions
    {
        public static Movie ToMovie(this MovieDetailDTO movieDetailDTO)
        {
            return new Movie(movieDetailDTO.Title, movieDetailDTO.Tagline, "https://image.tmdb.org/t/p/w500" + movieDetailDTO.Poster_path);
        }

        public static IEnumerable<Movie> ToMovieList(this MovieDetailResultListDTO movieDetailResultListDTO)
        {
            return movieDetailResultListDTO.results.Select(movie => movie.ToMovie());
        }
    }
}
