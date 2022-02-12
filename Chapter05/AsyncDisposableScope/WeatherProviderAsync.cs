using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDisposableScope
{
    public interface IWeatherProviderAsync
    {
        int GetCurrentTemperatire();
    }

    public class WeatherProviderAsync : IWeatherProviderAsync, IAsyncDisposable
    {
        public ValueTask DisposeAsync() {
            Console.WriteLine("Disposing asynchronously");
            return default;
        }

        public int GetCurrentTemperatire()
        {
            return new Random().Next(110);
        }
    }


}
