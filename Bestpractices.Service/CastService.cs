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
    public class CastService : ICastService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerAgent _loggerAgent;

        public CastService(ILoggerAgent loggerAgent)
        {
            _loggerAgent = loggerAgent ?? throw new ArgumentNullException(nameof(loggerAgent));

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.AZURE_FUNC_MOVIE_API)
            };
        }

        public async Task<IEnumerable<Cast>> GetMovieCredits(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            _loggerAgent.Information($"Call movie cast for movie id: {id}");

            var response = await _httpClient.GetAsync($"MovieCast?code=iu4wd97KvLQV4GlZaRvpljfUllHKReay7G6RosCpsqBRxZvZLu1aLw==&Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var cast = JsonConvert.DeserializeObject<CastResult>(content);

                if (cast != null)
                    return cast.ToModel();

                _loggerAgent.Error($"CastService: GetMovieCredits for movie id {id} deserialize failed");
                throw new InvalidCastException(nameof(cast));

            }

            _loggerAgent.Error($"MovieService: GetMovie called with id {id} failed. Status code: {response.StatusCode}");
            return null;
        }
    }
}
