using System.Threading.Tasks;

namespace Quickstart.AspNetCore.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeather> GetWeatherAsync(float lat, float lon);
    }
}