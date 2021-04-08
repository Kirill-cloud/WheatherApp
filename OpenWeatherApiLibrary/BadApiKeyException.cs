using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherApiLibrary
{
    public class BadApiKeyException : Exception
    {
        public string FailureApi { get; private set; }
        public BadApiKeyException(string apiKey)
        {
            FailureApi = apiKey;
        }
    }
}
