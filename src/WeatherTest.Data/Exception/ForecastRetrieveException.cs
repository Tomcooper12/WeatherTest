using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherTest.Data.Exception
{

    [Serializable]
    public class ForecastRetrieveException : System.Exception
    {
        public ForecastRetrieveException() { }
        public ForecastRetrieveException(string message) : base(message) { }
        public ForecastRetrieveException(string message, System.Exception inner) : base(message, inner) { }
    }
}
