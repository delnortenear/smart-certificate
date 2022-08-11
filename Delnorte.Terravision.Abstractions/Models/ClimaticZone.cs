
using System;
using Delnorte.Shared.Contracts;
using Newtonsoft.Json;

namespace Delnorte.Terravision.Abstractions.Models
{
    /// <summary>
    /// Represents an native climatic zone object
    /// </summary>
    public class ClimaticZone : IIdentified, IIdentified<Guid>, IAssignableTo<ClimaticZone>
    {
        public const string TypeName = "climaticzone";

        [JsonProperty("key")]
        public NativeKey<Guid> ZoneId { get; set; }

        [JsonProperty("name")]
        public string ZoneName { get; set; }

        [JsonProperty("avgtemp")]
        public double AgvTemp { get; set; }

        [JsonProperty("avghm")]
        public double AvgHumidity { get; set; }

        [JsonProperty("avgrain")]
        public double AvgRainFall { get; set; }

        public ClimaticZone()
        {
        }

        [JsonConstructor]
        internal ClimaticZone([JsonProperty("key")] NativeKey<Guid> zoneid
            , [JsonProperty("name")] string zoneName
            , [JsonProperty("avgtemp")] double avgTemp
            , [JsonProperty("avghm")] double avgHum
            , [JsonProperty("avgrain")] double avgRainFall) : this(zoneid.Id, zoneName, avgTemp, avgHum, avgRainFall)
        {
        }

        internal ClimaticZone([JsonProperty("key")] Guid zoneid
            , [JsonProperty("name")] string zoneName
            , [JsonProperty("avgtemp")] double avgTemp
            , [JsonProperty("avghm")] double avgHum
            , [JsonProperty("avgrain")] double avgRainFall)
        {
            this.ZoneId = new NativeKey<Guid>(zoneid);
            this.ZoneName = zoneName;
            this.AvgHumidity = avgHum;
            this.AgvTemp = avgTemp;
            this.AvgRainFall = avgRainFall;
        }

        #region IIdentified<Guid> region
        Guid IIdentified<Guid>.Id { get { return ZoneId.Id; } }

        ulong IIdentified<Guid>.Revision { get { return ZoneId.Revision; } }

        bool IEquatable<IIdentified<Guid>>.Equals(IIdentified<Guid> other)
        {
            return other == null ? false : this.ZoneId.Id.Equals(other.Id);
        }
        #endregion

        #region IIdentified region
        string IIdentified.Id { get { return ZoneId.Id.ToString("N"); } }

        ulong IIdentified.Revision { get { return ZoneId.Revision; } }

        bool IEquatable<IIdentified>.Equals(IIdentified other)
        {
            return other == null ? false : this.ZoneId.Id.Equals(other.Id);
        }
        #endregion

        #region IAssignableTo<ClimaticZone> members
        void IAssignableTo<ClimaticZone>.CopyPropertiesTo(ClimaticZone other, bool makeCopy)
        {
            other.AgvTemp = this.AgvTemp;
            other.AvgHumidity = this.AvgHumidity;
            other.AvgRainFall = this.AvgRainFall;
            other.ZoneName = this.ZoneName;

            if (makeCopy)
            {
                other.ZoneId = this.ZoneId;
            }
        }
        #endregion
        public static explicit operator Reference(ClimaticZone value)
        {
            var reference = new Reference()
            {
                Title = value.ZoneName,
                Type = TypeName,
                Url = $"/climazone/{value.ZoneId.Id}"
            };

            return reference;
        }
    }
}
