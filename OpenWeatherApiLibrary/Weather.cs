using Newtonsoft.Json;

namespace OpenWeatherApiLibrary
{
    public class Weather
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("icon")]
        public string IconName { get; set; }

    }
}