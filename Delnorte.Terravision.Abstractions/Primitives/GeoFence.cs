
using System;
using Delnorte.Shared.Contracts;
using Newtonsoft.Json;


namespace Delnorte.Terravision.Abstractions.Primitives
{
    public readonly struct GeoFence : IIdentified, IIdentified<Guid>
    {
        /// <summary>
        /// Defines an uniguq zone identifier
        /// </summary>
        [JsonProperty("key")] public readonly NativeKey<Guid> Id;

        /// <summary>
        /// Denines a custom area points
        /// </summary>
        [JsonProperty("gps")] public readonly Gps[] Points;

        /// <summary>
        /// Denies a zone type level, luxury, dangerouse, crima etc
        /// </summary>
        [JsonProperty("zonetype")] public readonly uint ZoneType;

        /// <summary>
        /// Defines <see cref="ZoneType"/> impact level. 0 neutral 255 maximal impact
        /// </summary>
        [JsonProperty("impact")] public readonly byte Level;

        /// <summary>
        /// Defines zone color in ARGB
        /// </summary>
        [JsonProperty("color")] public readonly int Color;

        /// <summary>
        /// Gets or sets zone desciption
        /// </summary>
        [JsonProperty("note")] public readonly string Comment;

        public GeoFence([JsonProperty("key")] Guid id
            , [JsonProperty("zonetype")] uint type
            , [JsonProperty("impact")] byte level
            , [JsonProperty("color")] int color
            , [JsonProperty("note")] string comment
            , [JsonProperty("gps")] Gps[] points)
        {
            this.Id = new NativeKey<Guid>(id);
            this.ZoneType = type;
            this.Level = level;
            this.Color = color;
            this.Comment = comment;
            this.Points = points;
        }

        public GeoFence(NativeKey<Guid> id, uint type, byte level, int color, string comment, Gps[] points)
        {
            this.Id = id;
            this.ZoneType = type;
            this.Level = level;
            this.Color = color;
            this.Comment = comment;
            this.Points = points;
        }

        public GeoFence EditZone(Gps[] points)
        {
            var newKey = new NativeKey<Guid>(Id.Id, (uint)(Id.Revision + 1));
            return new GeoFence(newKey, ZoneType, Level, Color, Comment, points);
        }

        public GeoFence EditZone(uint type, byte level, string comment)
        {
            var newKey = new NativeKey<Guid>(Id.Id, (uint)(Id.Revision + 1));
            return new GeoFence(Id.Id, type, level, Color, comment, Points);
        }

        public GeoFence EditZone(uint type, byte level, int color, string comment, Gps[] points)
        {
            var newKey = new NativeKey<Guid>(Id.Id, (uint)(Id.Revision + 1));
            return new GeoFence(Id.Id, type, level, color, comment, points);
        }

        public static GeoFence Parse(string value)
        {
            return JsonConvert.DeserializeObject<GeoFence>(value);
        }


        public override string ToString()
        {
            return this.ToString(false);
        }

        public string ToString(bool indented = false)
        {
            return JsonConvert.SerializeObject(this, indented ? Formatting.Indented : Formatting.None);
        }


        #region IIdentified<Guid> region
        Guid IIdentified<Guid>.Id { get { return Id.Id; } }

        ulong IIdentified<Guid>.Revision { get { return Id.Revision; } }

        bool IEquatable<IIdentified<Guid>>.Equals(IIdentified<Guid> other)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IIdentified region
        string IIdentified.Id { get { return Id.Id.ToString("N"); } }

        ulong IIdentified.Revision { get { return Id.Revision; } }

        bool IEquatable<IIdentified>.Equals(IIdentified other)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    internal sealed class GeoFenceJsonAdapter : IJsonConvertAdapter
    {
        public static readonly IJsonConvertAdapter Instance = new GeoFenceJsonAdapter();

        private GeoFenceJsonAdapter()
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
