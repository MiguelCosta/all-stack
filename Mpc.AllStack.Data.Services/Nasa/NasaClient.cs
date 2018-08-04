namespace Mpc.AllStack.Data.Services.Nasa
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Mpc.AllStack.Domain.Models;
    using Mpc.AllStack.Infrastructure.CrossCutting.Settings;
    using Newtonsoft.Json;

    public class NasaClient : INasaClient
    {
        private readonly HttpClient _httpClient;
        private readonly NasaSettings _nasaConfiguration;

        public NasaClient(INasaClientFactory nasaClientFactory, NasaSettings nasaConfiguration)
        {
            _httpClient = nasaClientFactory.Create();
            _nasaConfiguration = nasaConfiguration;
        }

        public async Task<NasaImage> GetDayImageAsync()
        {
            var today = DateTime.UtcNow;
            var url = GetUrlWithApiKey() + $"&date={today.ToString("yyyy-MM-dd")}";

            var response = await _httpClient.GetStringAsync(url).ConfigureAwait(false);

            var image = JsonConvert.DeserializeObject<NasaImage>(response);

            return image;
        }

        private string GetUrlWithApiKey()
        {
            return $"{_nasaConfiguration.BaseUrl}apod?api_key={_nasaConfiguration.ApiKey}";
        }
    }
}
