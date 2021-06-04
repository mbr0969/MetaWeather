using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MetaWeather.Models;

namespace MetaWeather {
    public class MetaWeatherClient {
        private readonly HttpClient _client;

        public MetaWeatherClient(HttpClient client) {
            _client = client;
        }

      
        public async Task<WeatherLocation[]> GetLocation(string name, CancellationToken cancel = default) {
            return await _client.GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={name}", /* _jsonOptions,*/cancel).ConfigureAwait(false);
        }

        public async Task<WeatherLocation[]> GetLocation((double Latitude, double Longitude) Location, CancellationToken cancel = default) {

            var request = $"/api/location/search/?lattlong={Location.Latitude.ToString(CultureInfo.InvariantCulture)},{Location.Longitude.ToString(CultureInfo.InvariantCulture)}";


            var l = await _client.GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?lattlong={Location.Latitude.ToString(CultureInfo.InvariantCulture)},{Location.Longitude.ToString(CultureInfo.InvariantCulture)}"
                    , cancel).ConfigureAwait(false);

            return l;


        }

    }  
}
