using Newtonsoft.Json;

namespace OpenWeatherApiLibrary
{
    public class WindInfo
    {

        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public double Degrees { get; set; }

    }
}