using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherTest.Data.Model;

namespace WeatherTest.Data
{
    /// <summary>
    /// Interface to weather api
    /// </summary>
    public interface IWeatherData
    {
        /// <summary>
        /// Get the forecast for a given location
        /// </summary>
        /// <param name="Location"></param>
        /// <returns></returns>
        Task<List<WeatherDataQuery>> GetForecastsAsync(string Location);
    }
}
