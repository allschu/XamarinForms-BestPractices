﻿using BestPractices.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestPractices.Models.Extensions
{
    internal static class TrendingMoviesExtensions
    {
        public static MovieList ToModel(this Movie movie)
        {
           return new MovieList(movie.Id, movie.Title, movie.Tagline, movie.Image_path, movie.Release_Date);
        }

        public static List<MovieList> ToModel(this IEnumerable<Movie> movies)
        {
            return movies.Select(x => x.ToModel()).ToList();
        }
    }
}
