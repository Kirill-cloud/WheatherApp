using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherApiLibrary
{
    public class BadSityNameException : Exception
    {
        public string FailureName { get; private set; }
        public BadSityNameException(string name)
        {
            FailureName = name;
        }
        public BadSityNameException() 
        {
        
        }
    }
}
