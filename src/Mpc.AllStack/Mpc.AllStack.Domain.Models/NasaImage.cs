namespace Mpc.AllStack.Domain.Models
{
    using System;
    using Newtonsoft.Json;

    public class NasaImage
    {
        public DateTime Date { get; set; }

        public string Explanation { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}
