using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherTest.SampleApp.Process;
using WeatherTest.Data;
using WeatherTest.SampleApp.Model;
using WeatherTest.Data.Web;

namespace WeatherTest.SampleApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<WeatherResult> GetAsync(string location, int speed, int temp)
        {
            var weatherApp = new WeatherAggregate(speed, temp, new WeatherData());
            return await weatherApp.GetAggregateAsync(location);
        }
    }
}
