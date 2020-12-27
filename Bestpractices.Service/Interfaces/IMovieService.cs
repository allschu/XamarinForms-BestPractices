using BestPractices.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bestpractices.Service.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetTrendingMovies();
    }
}
