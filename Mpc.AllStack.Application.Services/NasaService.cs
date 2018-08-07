namespace Mpc.AllStack.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using Mpc.AllStack.Application.Dto;
    using Mpc.AllStack.Data.Services.Nasa;
    using Mpc.AllStack.Infrastructure.CrossCutting.Cache;

    public class NasaService : INasaService
    {
        private ICacheProvider _cacheProvider;
        private INasaClient _nasaClient;

        public NasaService(INasaClient nasaClient, ICacheProvider cacheProvider)
        {
            _nasaClient = nasaClient;
            _cacheProvider = cacheProvider;
        }

        public async Task<NasaImage> GetDayImageAsync()
        {
            var model = await _cacheProvider
                .GetAsync("today", new TimeSpan(0, 5, 0), () => _nasaClient.GetDayImageAsync())
                .ConfigureAwait(false);

            return new NasaImage
            {
                Date = model.Date,
                Description = model.Explanation,
                MediaType = model.MediaType,
                Title = model.Title,
                Url = model.Url
            };
        }
    }
}
