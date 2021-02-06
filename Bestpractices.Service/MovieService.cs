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

                if (movie != null) 
                    return movie.ToMovieDetail();
                
                _loggerAgent.Error($"MovieService: GetMovie with id {id} deserialize failed");
                throw new InvalidCastException(nameof(movie));

            }
            else
            {
                _loggerAgent.Error($"MovieService: GetMovie called with id {id} failed. Statuscode: {response.StatusCode}");
                return new MovieDetail();
            }
        }

        public async Task<IEnumerable<MovieSearchResult>> GetMovieRecommendations(int movieId)
        {
            _loggerAgent.Information($"call GetMovieRecommendations with id {movieId}");
            var response = await _httpClient.GetAsync($"Recommendations?code=4MH6Jsg32V1cmm3cMd4FHxKhfA/SfvVpAat5mSj602M26pXreYD0AQ==&Id={movieId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var movies = JsonConvert.DeserializeObject<MovieSearchResultListDTO>(content);

                if (movies != null) 
                    return movies.ToMovieSearchResultList();
                
                _loggerAgent.Error($"MovieService: GetMovieRecommendations DeserializeObject failed, is the contract still the same?");
                throw new InvalidCastException(nameof(movies));

            }
            else
            {
                _loggerAgent.Error($"MovieService: GetMovieRecommendations failed. Status code: {response.StatusCode}");
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

                var trendingMovies = JsonConvert.DeserializeObject<MovieDetailResultListDTO>(content);

                if (trendingMovies == null)
                    throw new InvalidCastException(nameof(trendingMovies));

                return trendingMovies.ToMovieList();
            }
            else
            {
                _loggerAgent.Error($"MovieService: GetTrendingMovies failed. Status code: {response.StatusCode}");
                throw new InvalidOperationException();
            }
        }

        public async Task<IEnumerable<MovieSearchResult>> SearchMovie(string searchQuery, int page)
        {
            var response = await _httpClient.GetAsync($"SearchMovies?code=bwvSvS1C9AAoccO4/yvR5fRH7Z0LzHWbN1aGlMh2l7pNDkbkeea6EA==&search={searchQuery}&page={page}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var searchMovies = JsonConvert.DeserializeObject<MovieSearchResultListDTO>(content);

                if (searchMovies != null) 
                    return searchMovies.ToMovieSearchResultList();

                _loggerAgent.Error($"MovieService: SearchMovie DeserializeObject failed, is the contract still the same?");
                throw new InvalidCastException(nameof(searchMovies));

            }
            else
            {
                _loggerAgent.Error($"MovieService: SearchMovie failed. Status code: {response.StatusCode} query: {searchQuery}");
                throw new InvalidOperationException();
            }
        }
    }
}
