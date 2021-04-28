﻿using Newtonsoft.Json;

namespace OpenWeatherApiLibrary
{
    public class TemperatureInfo
    {

        [JsonProperty("day")]
        public double Day { get; set; }
        [JsonProperty("night")]
        public double Night { get; set; }
        [JsonProperty("eve")]
        public double Evening { get; set; }
        [JsonProperty("morn")]
        public double Morning { get; set; }
        [JsonProperty("min")]
        public double Min { get; set; }
        [JsonProperty("max")]
        public double Max { get; set; }

    }
}