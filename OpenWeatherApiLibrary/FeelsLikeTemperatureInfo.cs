using Newtonsoft.Json;

namespace OpenWeatherApiLibrary
{
    public class FeelsLikeTemperatureInfo
    {
        [JsonProperty("day")]
        public double Day { get; set; }
        [JsonProperty("night")]
        public double Night { get; set; }
        [JsonProperty("eve")]
        public double Evening { get; set; }
        [JsonProperty("morn")]
        public double Morning { get; set; }

    }
}