using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using WeatherTest.Data.Model;

namespace WeatherTest.Data.Web
{
    /// <summary>
    /// Json converter class to convert Json string to WeatherDataQuery object
    /// </summary>
    public class WeatherDataConverter : JsonConverter
    {
        /// <summary>
        /// Check can convert to object
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(WeatherDataQuery));
        }

        /// <summary>
        /// Read and convert Json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            var result = new WeatherDataQuery();

            foreach (var jItem in jo.Children())
            {
                if (jItem.Path.ToLower().Contains("fahren")) result.TempMetric = (int)Unit.Temperature.F;
                if (jItem.Path.ToLower().Contains("celsi")) result.TempMetric = (int)Unit.Temperature.C;
                if (jItem.Path.ToLower().Contains("mph")) result.SpeedMetric = (int)Unit.Speed.MPH;
                if (jItem.Path.ToLower().Contains("kph")) result.SpeedMetric = (int)Unit.Speed.KPH;

                if (jItem.Path.ToLower().Contains("temp")) result.Temperature = jItem.First.Value<double>();
                if (jItem.Path.ToLower().Contains("wind")) result.WindSpeed = jItem.First.Value<double>();
                if (jItem.Path.ToLower().Contains("where")) result.Location = jItem.First.Value<string>();
                if (jItem.Path.ToLower().Contains("location")) result.Location = jItem.First.Value<string>();
            }

            return result;
        }

        /// <summary>
        /// Write
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
