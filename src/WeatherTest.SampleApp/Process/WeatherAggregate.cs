using System;
using System.Linq;
using WeatherTest.Data;
using WeatherTest.Data.Exception;
using WeatherTest.SampleApp.Model;

namespace WeatherTest.SampleApp.Process
{
    /// <summary>
    /// Aggregate Class to get and average data
    /// </summary>
    public class WeatherAggregate
    {
        private int SpeedUnit { get; set; }
        private int TempUnit { get; set; }
        private IWeatherData DataAccess { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="temp"></param>
        /// <param name="data"></param>
        public WeatherAggregate(int speed, int temp, IWeatherData data)
        {
            SpeedUnit = speed;
            TempUnit = temp;
            //Dep inject
            DataAccess = data;
        }

        /// <summary>
        /// Get aggregate model
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<WeatherResult> GetAggregateAsync(string location)
        {
            try
            {
                var dataSet = await DataAccess.GetForecastsAsync(location);
                using (var enumerator = dataSet.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        if (current.SpeedMetric != SpeedUnit)
                        {
                            current.WindSpeed = current.SpeedMetric == (int)Unit.Speed.MPH ? MphToKph(current.WindSpeed) : KphToMph(current.WindSpeed);
                        }

                        if (current.TempMetric != TempUnit)
                        {
                            current.Temperature = current.TempMetric == (int)Unit.Temperature.C ? CToF(current.Temperature) : FToC(current.Temperature);
                        }
                    }
                }

                return new WeatherResult
                {
                    Location = dataSet.Select(r => r.Location).FirstOrDefault(),
                    Temperature = (int)Math.Round(dataSet.Average(r => r.Temperature)),
                    WindSpeed = Math.Round(dataSet.Average(r => r.WindSpeed), 1, MidpointRounding.ToEven),
                    Success = true
                };
            } catch (ForecastRetrieveException)
            {
                return new WeatherResult { Success = false };
            }
        }

        /// <summary>
        /// Convert Celsius to Fahrenheit 
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private double CToF(double temp)
        {
            return ((9.0 / 5.0) * temp) + 32;
        }

        /// <summary>
        /// Convert Fahrenheit to Celsius
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private double FToC(double temp)
        {
            return (5.0 / 9.0) * (temp - 32);
        }

        /// <summary>
        /// Convert Kilometers per Hour to Miles per Hour
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        private double KphToMph(double speed)
        {
            return (speed * 0.621371192);
        }

        /// <summary>
        /// Convert Miles per Hour to Kilometers per Hour
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        private double MphToKph(double speed)
        {
            return (speed * 1.609344);
        }
    }
}
