using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherTest.Data.Exception;
using WeatherTest.Data.Model;
using WeatherTest.Data.Web;

namespace WeatherTest.Data
{
    /// <summary>
    /// Data class implementation of IWeatherData
    /// </summary>
    public class WeatherData : IWeatherData
    {
        //hardcoded for test purposes
        private const string _ACCU_URL = "http://localhost:50370/";
        private const string _BBC_URL = "http://localhost:50341/";
        private const string _RELATIVE = "api/weather/";

        /// <summary>
        /// Get forecasts from data sources
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<List<WeatherDataQuery>> GetForecastsAsync(string location)
        {
            //data list
            var weatherData = new List<WeatherDataQuery>();
            var client = new RestClient(TimeSpan.FromMinutes(1));

            //get data
            var bbcResponse = await client.GetAsync(_BBC_URL, _RELATIVE + location);
            var accuResponse = await client.GetAsync(_ACCU_URL, _RELATIVE + location);

            if (!bbcResponse.IsSuccessStatusCode && !accuResponse.IsSuccessStatusCode)
            {
                throw new ForecastRetrieveException("Error getting forecast information from data source");
            }

            //deserialize json
            if (bbcResponse.IsSuccessStatusCode)
            {
                //get json string
                var bbcStr = bbcResponse.Content.ReadAsStringAsync().Result;
                //deserialize
                if (!String.IsNullOrWhiteSpace(bbcStr))
                    weatherData.Add(JsonConvert.DeserializeObject<WeatherDataQuery>(bbcStr));
            }

            if (accuResponse.IsSuccessStatusCode)
            {
                //get json string
                var accuStr = accuResponse.Content.ReadAsStringAsync().Result;
                //deserialize
                if (!String.IsNullOrWhiteSpace(accuStr))
                    weatherData.Add(JsonConvert.DeserializeObject<WeatherDataQuery>(accuStr));
            }

            //return
            return weatherData;
        }
    }
}
