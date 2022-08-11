using System;
using System.Linq;

using Delnorte.Shared.Contracts;
using Newtonsoft.Json;

namespace Delnorte.Terravision.Abstractions.Primitives
{
    /// <summary>
    /// Denies a geometry props of area
    /// </summary>
    public readonly struct Geometry
    {
        /// <summary>
        /// Denines a north orientation from 0 to 360
        /// </summary>
        [JsonProperty("angle")] public readonly double Angle;

        /// <summary>
        /// Denines a custom area points
        /// </summary>
        [JsonProperty("points")] public readonly Gps[] Points;

        /// <summary>
        /// Defines an overall area 
        /// </summary>
        [JsonProperty("area")] public readonly double Area;

        public Geometry([JsonProperty("area")] double area
            , [JsonProperty("angle")] double angle = 0
            , [JsonProperty("points")] params Gps[] points)
        {
            //TODO: Add to resource file
            Guard.AssertLogic<ArgumentException>(() => points.Length > 2, err => new ArgumentException(err), "Area requires at least 3 points");

            this.Area = area;
            this.Angle = angle;
            this.Points = points.ToArray();
        }

        /// <summary>
        /// Creates a new instance of geometry with updates points data
        /// </summary>
        /// <param name="points">array of geometry points</param>
        /// <returns>New object</returns>
        public Geometry UpdatePoints(params Gps[] points)
        {
            //TODO: Add to resource file
            Guard.AssertLogic<ArgumentException>(() => points.Length > 2, err => new ArgumentException(err), "Area requires at least 3 points");
            return new Geometry(this.Area, this.Angle, points);
        }

        public static Geometry Parse(string value)
        {
            return JsonConvert.DeserializeObject<Geometry>(value);
        }



        public override string ToString()
        {
            return this.ToString(false);
        }

        public string ToString(bool indented = false)
        {
            return JsonConvert.SerializeObject(this, indented ? Formatting.Indented : Formatting.None);
        }
    }

    internal sealed class GeometryJsonAdapter : IJsonConvertAdapter
    {
        public static readonly IJsonConvertAdapter Instance = new GeometryJsonAdapter();

        private GeometryJsonAdapter()
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
