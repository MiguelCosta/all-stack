namespace Mpc.AllStack.Data.Services.Nasa
{
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class NasaClientFactory : INasaClientFactory
    {
        public HttpClient Create()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
