using System.Threading.Tasks;

namespace Quickstart.AspNetCore.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetWeatherAsync(double lat, double lon);
    }
}