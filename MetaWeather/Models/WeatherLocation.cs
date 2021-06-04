using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MetaWeather.Service;

namespace MetaWeather.Models
{
    public class WeatherLocation {
        //[{"title":"St Petersburg","location_type":"City","woeid":2123260,"latt_long":"59.932739,30.306721"}]

        [JsonPropertyName("woeid")] 
        public int Id { get; set; }

        [JsonPropertyName("title")] 
        public string Title { get; set; }

        [JsonPropertyName("location_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LocationType Type { get; set; }

        [JsonPropertyName("latt_long")]
        [JsonConverter(typeof(JsonCoordinateConverter))]
        public (double Latitude, double Longitude) Location { get; set; }

        [JsonPropertyName("distance")] 
        public int Distance { get; set; }

        public override string ToString() {
            return $"[{Id}]{Title}({Type}):({Location}) ({Distance})";
        }


    }
}
