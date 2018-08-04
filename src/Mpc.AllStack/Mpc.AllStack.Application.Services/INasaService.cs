namespace Mpc.AllStack.Application.Services
{
    using System.Threading.Tasks;
    using Mpc.AllStack.Application.Dto;

    public interface INasaService
    {
        Task<NasaImage> GetDayImageAsync();
    }
}
