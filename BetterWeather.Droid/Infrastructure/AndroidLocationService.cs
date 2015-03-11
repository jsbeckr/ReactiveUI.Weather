using System.Threading.Tasks;
using BetterWeather.Data.Infrastructure;
using BetterWeather.Data.Interfaces;
using Geolocator.Plugin;

namespace BetterWeather.Droid.Infrastructure
{
    public class AndroidLocationService : ILocationService
    {
        public async Task<GpsPosition> GetCurrentPositionAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(timeout: 5000);

            return new GpsPosition(position.Longitude, position.Latitude);
        }
    }
}