using Bestpractices.Service.Contract;
using BestPractices.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bestpractices.Service.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetTrendingMovies();
        Task<IEnumerable<MovieSearchResult>> SearchMovie(string searchQuery, int page);
        Task<MovieDetail> GetMovie(int id);
    }
}
