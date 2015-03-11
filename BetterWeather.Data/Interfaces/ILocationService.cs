using System.Threading.Tasks;
using BetterWeather.Data.Infrastructure;

namespace BetterWeather.Data.Interfaces
{
    public interface ILocationService
    {
        Task<GpsPosition> GetCurrentPositionAsync();
    }
}