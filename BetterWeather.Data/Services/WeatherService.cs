using System.Net.Http;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace BetterWeather.Data.Services
{
    public class WeatherService
    {
        public async Task<dynamic> GetCurrentWeather(double lon, double lat)
        {
            var client = new RestClient("http://api.openweathermap.org/data/2.5/");
            var request = new RestRequest(string.Format("weather", lat, lon), HttpMethod.Get);
            request.AddParameter("lon", lon);
            request.AddParameter("lat", lat);
            request.AddParameter("units", "metric");

            var result = await client.Execute<dynamic>(request);
            return result.Data;
        }
    }
}
