using System.Collections.Generic;

namespace OpenWeatherApiLibrary
{
    public class DailyInfo
    {
        //маппер или комментарий
        public ulong dt{ get; set; }
        public TemperatureInfo temp { get; set; }
        public FeelsLikeTemperatureInfo feels_like { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double wind_speed { get; set; }
        public double wind_deg{ get; set; }
        public List<Weather> weather { get; set; }
    }
}