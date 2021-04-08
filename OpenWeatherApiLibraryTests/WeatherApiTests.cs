using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenWeatherApiLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OpenWeatherApiLibrary.Tests
{
    [TestClass()]
    public class WeatherApiTests
    {
        [TestMethod()]
        public void NormalFlowGetForcastByCityNameTest()
        {
            string apiKey = "82b797b6ebc625032318e16f1b42c016";
            WeatherApi client = new WeatherApi(apiKey);
            string sityName = "Novosibirsk";

            string assertedTimezone = "Asia/Novosibirsk";

            var actual = client.GetForcastByCityName(sityName);

            Assert.IsNotNull(actual);
            Assert.AreEqual(assertedTimezone,actual.timezone);
        }
        [TestMethod()]
        public void BadCityGetForcastByCityNameTest()
        {
            string apiKey = "82b797b6ebc625032318e16f1b42c016";
            WeatherApi client = new WeatherApi(apiKey);
            string sityName = "7";

            Assert.ThrowsException<BadSityNameException>(()=> client.GetForcastByCityName(sityName));
        }
        [TestMethod()]
        public void BadApiKeyGetForcastByCityNameTest()
        {
            string apiKey = "0";
            WeatherApi client = new WeatherApi(apiKey);
            string sityName = "Novosibirsk";

            Assert.ThrowsException<BadApiKeyException>(() => client.GetForcastByCityName(sityName));
        }

        [TestMethod()]
        public void NoConnectionGetForcastByCityNameTest()
        {
            string apiKey = "82b797b6ebc625032318e16f1b42c016";
            WeatherApi client = new WeatherApi(apiKey);
            string sityName = "Novosibirsk";

            System.Diagnostics.Process.Start("ipconfig", "/release");

            Assert.ThrowsException<NoConnectionException>(()=>client.GetForcastByCityName(sityName));

            System.Diagnostics.Process.Start("ipconfig", "/renew");
        }

    }
}