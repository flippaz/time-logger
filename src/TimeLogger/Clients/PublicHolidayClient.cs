using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TimeLogger.Models;
using TimeLogger.Settings;

namespace TimeLogger.Clients
{
    public class PublicHolidayClient : IPublicHolidayClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PublicHolidayClient> _logger;
        private readonly string _resourceId;
        private readonly string _state;

        public PublicHolidayClient(IOptions<PublicHolidaySettings> settings, ILogger<PublicHolidayClient> logger)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(settings.Value.ApiUrl),
                Timeout = TimeSpan.FromMinutes(5)
            };
            _resourceId = settings.Value.ResourceId;
            _state = settings.Value.State;
            _logger = logger;
        }

        public async Task<PublicHolidayResponse> GetHolidays()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"?resource_id={_resourceId}&q={_state}"))
            {
                _logger.LogInformation($"Sending to {_httpClient.BaseAddress}");

                using (HttpResponseMessage response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PublicHolidayResponse>(content);
                }
            }
        }
    }
}