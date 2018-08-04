namespace Mpc.AllStack.Data.Services.Nasa
{
    using System.Net.Http;

    public interface INasaClientFactory
    {
        HttpClient Create();
    }
}
