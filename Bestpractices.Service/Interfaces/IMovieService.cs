using BestPractices.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bestpractices.Service.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetTrendingMovies();
        Task<Movie> GetMovie(int id);
    }
}
