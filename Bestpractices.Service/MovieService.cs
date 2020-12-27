using Bestpractices.Service.Interfaces;
using BestPractices.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bestpractices.Service
{
    public class MovieService : IMovieService
    {
        public Task<IEnumerable<Movie>> GetTrendingMovies()
        {
            throw new NotImplementedException();
        }
    }
}
