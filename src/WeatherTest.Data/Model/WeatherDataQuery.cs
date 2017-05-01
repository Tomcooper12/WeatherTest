using Newtonsoft.Json;
using WeatherTest.Data.Web;

namespace WeatherTest.Data.Model
{
    /// <summary>
    /// Weather result object
    /// </summary>
    [JsonConverter(typeof(WeatherDataConverter))]
    public class WeatherDataQuery
    {
        public double Temperature { get; set; }
        public double WindSpeed { get; set; }
        public string Location { get; set; }
        public int TempMetric { get; set; }
        public int SpeedMetric { get; set; }
    }
}
