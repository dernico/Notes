using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class PlaceModel
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("formatted_address")]
        public string Address { get; set; }

        [JsonProperty("formatted_phone_number")]
        public string PhoneNumber { get; set; }

        [JsonProperty("geometry")]
        public GeometryModel Geometry { get; set; }
        
        [JsonProperty("weekday_text")]
        public List<string> OpeningsWeekdays { get; set; }



        public class GeometryModel
        {
            [JsonProperty("location")]
            public LocationModel Location;
        }

        public class LocationModel
        {
            [JsonProperty("lat")]
            public string Lat;

            [JsonProperty("lng")]
            public string Long;
        }
    }
}
