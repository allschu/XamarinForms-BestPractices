using BestPractices.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bestpractices.Service.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetTrendingMovies();
        Task<MovieDetail> GetMovie(int id);
    }
}
