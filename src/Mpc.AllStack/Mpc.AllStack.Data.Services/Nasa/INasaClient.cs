namespace Mpc.AllStack.Data.Services.Nasa
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mpc.AllStack.Domain.Models;

    public interface INasaClient
    {
        Task<NasaImage> GetDayImageAsync();
    }
}
