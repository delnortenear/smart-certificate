using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delnorte.Terravision.Abstractions
{
    public interface IJsonConvertAdapter
    {
        object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);

        void WriteJson(JsonWriter writer, object value, JsonSerializer serializer);
    }
}
