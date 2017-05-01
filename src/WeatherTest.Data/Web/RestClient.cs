using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherTest.Data.Web
{
    /// <summary>
    /// Rest client to query api's
    /// </summary>
    public class RestClient
    {
        private TimeSpan _timeout { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        public RestClient(TimeSpan timeout)
        {
            _timeout = timeout;
        }

        /// <summary>
        /// Call an async GET request
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string uri, string path)
        {
            try
            {
                var client = new HttpClient()
                {
                    Timeout = _timeout
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri(uri);

                return await client.GetAsync(path);

            } catch (System.Net.WebException)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);

            } catch (TaskCanceledException)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout);

            } catch (HttpRequestException)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }

    }
}
