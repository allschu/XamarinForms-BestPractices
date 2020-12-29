using Bestpractices.Service.Contract;
using Bestpractices.Service.Extensions;
using Bestpractices.Service.Interfaces;
using BestPractices.Common.Models;
using BestPractices.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bestpractices.Service
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerAgent _loggerAgent;

        public MovieService(ILoggerAgent loggerAgent)
        {
            _loggerAgent = loggerAgent ?? throw new ArgumentNullException(nameof(loggerAgent));

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.AZURE_FUNC_MOVIE_API)
            };
        }

        public async Task<MovieDetail> GetMovie(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            _loggerAgent.Information($"Call movie with id: {id}");

            var response = await _httpClient.GetAsync($"MovieDetail?code=jEpGNqiIn9OlAcsNd5gNj2vfAoyqxfKOgt8ZxFqmPhrJJnaPh5vdSQ==&Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var movie = JsonConvert.DeserializeObject<MovieDetailDTO>(content);

                if (movie == null)
                {
                    _loggerAgent.Error($"MovieService: GetMovie with id {id} deserialize failed");
                    throw new InvalidCastException(nameof(movie));
                }

                return movie.ToMovieDetail();
            }
            else
            {
                _loggerAgent.Error($"MovieService: GetMovie called with id {id} failed. Statuscode: {response.StatusCode}");
                throw new InvalidOperationException();
            }
        }

        public async Task<IEnumerable<Movie>> GetTrendingMovies()
        {
            _loggerAgent.Information("Call trending movies");

            var response = await _httpClient.GetAsync("TrendingMovies?code=ApGoXkfiUHdliapJ232zvA1ArqHRk5hw29Jr6PM4dJYbpPvWq3OaaA==");                     
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var trendingmovies = JsonConvert.DeserializeObject<MovieDetailResultListDTO>(content);

                if (trendingmovies == null)
                    throw new InvalidCastException(nameof(trendingmovies));

                return trendingmovies.ToMovieList();
            }
            else
            {
                _loggerAgent.Error($"MovieService: GetTrendingMovies failed. Statuscode: {response.StatusCode}");
                throw new InvalidOperationException();
            }
        }
    }
}
