using Newtonsoft.Json;
using System.Collections.Generic;

namespace OpenWeatherApiLibrary
{
    public class DailyInfo
    {
        [JsonProperty("dt")]
        public ulong DateUnix { get; set; }

        [JsonProperty("temp")]
        public TemperatureInfo Temperature { get; set; }

        [JsonProperty("feels_like")]
        public FeelsLikeTemperatureInfo FeelsLikeTemperature { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }

        [JsonProperty("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonProperty("wind_deg")]
        public double WindDegees { get; set; }

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
    }
}