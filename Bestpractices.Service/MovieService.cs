using Bestpractices.Service.Interfaces;
using BestPractices.Common.Models;
using BestPractices.Logging;
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
                BaseAddress = new Uri("https://moviefunctions-allschu.azurewebsites.net/api/")
            };
        }


        public async Task<IEnumerable<Movie>> GetTrendingMovies()
        {
            var response = await _httpClient.GetAsync("TrendingMovies?code=ApGoXkfiUHdliapJ232zvA1ArqHRk5hw29Jr6PM4dJYbpPvWq3OaaA==");                     
            if (response.IsSuccessStatusCode)
            {

            }
        }
    }
}
