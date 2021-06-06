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

        public MetaWeatherClient(HttpClient client) => _client = client;


        public async Task<WeatherLocation[]> GetLocation(string name, CancellationToken cancel = default) {
            return await _client.GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?query={name}", /* _jsonOptions,*/cancel).ConfigureAwait(false);
        }

        public async Task<WeatherLocation[]> GetLocation((double Latitude, double Longitude) Location, CancellationToken cancel = default) {

            return  await _client.GetFromJsonAsync<WeatherLocation[]>($"/api/location/search/?lattlong={Location.Latitude.ToString(CultureInfo.InvariantCulture)},{Location.Longitude.ToString(CultureInfo.InvariantCulture)}"
                    , cancel).ConfigureAwait(false);
        }

        public async Task<LocationInfo> GetInfo(int woeId, CancellationToken cancel = default) {
            ///api/location/44418/

            return await _client.GetFromJsonAsync<LocationInfo>($"/api/location/{woeId.ToString(CultureInfo.InvariantCulture)}", cancel).ConfigureAwait(false);

        }

        public async Task<LocationInfo> GetInfo(WeatherLocation location, CancellationToken cancel = default) {
            return await GetInfo(location.Id, cancel);
        }
    }  
}
