using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MetaWeather.Models
{

    public class WeatherLocation : Location {
        //[{"title":"St Petersburg","location_type":"City","woeid":2123260,"latt_long":"59.932739,30.306721"}]

        [JsonPropertyName("distance")] 
        public int Distance { get; set; }

        public override string ToString() {
            return $"[{Id}]{Title}({Type}):({Coordinates}) ({Distance})";
        }


    }
}
