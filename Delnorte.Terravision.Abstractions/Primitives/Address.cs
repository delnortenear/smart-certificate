using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delnorte.Terravision.Abstractions.Primitives
{
    public readonly struct Address
    {
        [JsonProperty("country")]
        public readonly string Country;
        [JsonProperty("city")]
        public readonly string City;
        [JsonProperty("line")]
        public readonly string AddrLine;
        [JsonProperty("index")]
        public readonly string Index;
        [JsonProperty("state")]
        public readonly string State;

        [JsonProperty("district")]
        public readonly string District;


        public Address([JsonProperty("country")] string country
            , [JsonProperty("city")] string city
            , [JsonProperty("district")] string district
            , [JsonProperty("line")] string address
            , [JsonProperty("index")] string index
            , [JsonProperty("state")] string state)
        {
            this.District = district;
            this.City = city;
            this.AddrLine = address;
            this.Index = index;
            this.State = state;
            this.Country = country;
        }

        public static Address Parse(string value)
        {
            return JsonConvert.DeserializeObject<Address>(value);
        }

        internal string GetSityByCode(string cityCode)
        {
            throw new NotImplementedException();
        }

        public string GetAddressString()
        {
            var fields = new string[] {
                this.AddrLine,
                this.District,
                this.Index,
                this.Country,
                this.City,
                this.State
            };

            var addrStr = "";
            var ifFirst = true;
            foreach (var item in fields)
            {
                if (string.IsNullOrEmpty(item))
                    continue;
                if (ifFirst)
                    addrStr += item;
                else
                    addrStr += ", " + item;

                ifFirst = false;
            }

            return addrStr;
            //return $"{this.AddrLine != null ? (this.AddrLine + ","): ""} {District!=null?(District + ","): ""} {Index?? (Index + ",")} {Country}, {City} {State?? ("," + State)} ";
        }
    }

    internal sealed class AddressJsonAdapter : IJsonConvertAdapter
    {
        public static readonly IJsonConvertAdapter Instance = new AddressJsonAdapter();

        private AddressJsonAdapter()
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

    public static class USStatesConsts
    {
        public const ushort NY = 1;
        public const ushort MIA = 2;
    }
}
