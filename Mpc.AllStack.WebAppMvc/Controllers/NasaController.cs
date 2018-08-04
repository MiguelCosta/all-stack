namespace Mpc.AllStack.WebAppMvc.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Mpc.AllStack.Application.Services;

    public class NasaController : Controller
    {
        private readonly INasaService _nasaService;

        public NasaController(INasaService nasaService)
        {
            _nasaService = nasaService;
        }

        public async Task<IActionResult> Index()
        {
            var image = await this._nasaService.GetDayImageAsync().ConfigureAwait(false);
            return View(image);
        }
    }
}
