namespace Mpc.AllStack.Application.Services
{
    using System.Threading.Tasks;
    using Mpc.AllStack.Application.Dto;
    using Mpc.AllStack.Data.Services.Nasa;

    public class NasaService : INasaService
    {
        private INasaClient _nasaClient;

        public NasaService(INasaClient nasaClient)
        {
            _nasaClient = nasaClient;
        }

        public async Task<NasaImage> GetDayImageAsync()
        {
            var model = await _nasaClient.GetDayImageAsync().ConfigureAwait(false);

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
