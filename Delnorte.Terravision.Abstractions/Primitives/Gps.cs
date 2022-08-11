using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delnorte.Terravision.Abstractions.Primitives
{
    /// <summary>
    /// Representa Geo position object
    /// </summary>
    public readonly struct Gps
    {
        [JsonProperty("lat")] public readonly double Latitude;
        [JsonProperty("long")] public readonly double Longitude;
        [JsonProperty("lvl")] public readonly double SeaLevel;

        public Gps([JsonProperty("lat")] double lat, [JsonProperty("long")] double @long, [JsonProperty("lvl")] double sealv)
        {
            this.Latitude = lat;
            this.Longitude = @long;
            this.SeaLevel = sealv;
        }

        public static Gps Parse(string value)
        {
            return JsonConvert.DeserializeObject<Gps>(value);
        }

    }

    internal sealed class GpsJsonAdapter : IJsonConvertAdapter
    {
        public static readonly IJsonConvertAdapter Instance = new GpsJsonAdapter();

        private GpsJsonAdapter()
        {
        }

        public object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
