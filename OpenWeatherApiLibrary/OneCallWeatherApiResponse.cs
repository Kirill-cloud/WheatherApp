using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherApiLibrary
{
    public class OneCallWeatherApiResponse
    {
        [JsonProperty("timezone")]
        public string LocationName { get; set; }
        [JsonProperty("timezone_offset")]
        public ulong TimezoneOffset { get; set; }
        [JsonProperty("daily")]
        public List<DailyInfo> DailyInfo { get; set; }

    }
}
