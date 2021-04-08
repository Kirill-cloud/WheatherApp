using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherApiLibrary
{
    public class OneCallWeatherApiResponse
    {
        public string timezone { get; set; }
        public ulong timezone_offset { get; set; }
        public List<DailyInfo> daily { get; set; }

    }
}
