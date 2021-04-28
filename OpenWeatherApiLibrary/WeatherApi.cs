using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace OpenWeatherApiLibrary
{
    public class WeatherApi
    {
        string key;
        public WeatherApi(string apiKey) 
        {
            key = apiKey;
        }
        (double lat, double lon) GetCoords(string cityName) 
        {
            try
            {
                string url = String.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, key);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jsonString;
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    jsonString = streamReader.ReadToEnd();
                }

                var deserialized = JsonConvert.DeserializeObject<CurrentWeatherApiResponse>(jsonString);
                return (deserialized.Coord.Lat, deserialized.Coord.Lon);
            }
            catch (WebException e)
            {
                if (e.Message.Contains("404"))
                {
                    throw new BadSityNameException(cityName);
                }
                else if (e.Message.Contains("401"))
                {
                    throw new BadApiKeyException(key);
                }
                else //if (e.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    throw new NoConnectionException();
                }
            } 

        }
        public OneCallWeatherApiResponse GetForcastByCityName(string sityName) 
        {
            return GetForcastByCoords(GetCoords(sityName));
        }
        OneCallWeatherApiResponse GetForcastByCoords((double lat, double lon) coords) 
        {
            double lat = coords.lat;
            double lon = coords.lon;

            string url = String.Format("http://api.openweathermap.org/data/2.5/onecall?units=metric&lang=ru&lat={0}&lon={1}&appid={2}", lat, lon, key);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //string sResponse;
            //using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            //{
            //    sResponse = streamReader.ReadToEnd();
            //}
            //return JsonSerializer.Deserialize<OneCallWeatherApiResponse>(sResponse);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string jsonString;
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                jsonString = streamReader.ReadToEnd();
            }

            var deserialized = JsonConvert.DeserializeObject<OneCallWeatherApiResponse>(jsonString);
            return deserialized;
        }
    }
}
