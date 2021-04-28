using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherApiLibrary
{
    class CurrentWeatherApiResponse
    {
        [JsonProperty("coord")]
        public Coords Coord { get; set; }
    }
}
