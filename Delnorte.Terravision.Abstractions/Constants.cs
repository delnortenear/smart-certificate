
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json;
using System;
using Delnorte.Shared.Contracts;

namespace Delnorte.Terravision.Abstractions
{
    public static class CertificateStates
    {
        [JsonProperty("undefined")]
        public const int Undefined = 0;

        [JsonProperty("active")]
        public const int Active = 1;

        [JsonProperty("suspended")]
        public const int Suspended = 2;

        [JsonProperty("blocked")]
        public const int Blocked = 3;

        [JsonProperty("arrested")]
        public const int Arrested = 4;

        [JsonProperty("inpledge")]
        public const int InPledge = 5;

        public static string GetStatusName(int code)
        {
            switch (code)
            {
                case Active: return nameof(Active).ToLower();
                case Suspended: return nameof(Suspended).ToLower();
                case Blocked: return nameof(Blocked).ToLower();
                case Arrested: return nameof(Arrested).ToLower();
                case InPledge: return nameof(InPledge).ToLower();
                default:
                    return nameof(Undefined).ToLower();
            }
        }
    }

    public static class CaseStates
    {
        /// <summary>
        /// Case has undefined state
        /// </summary>
        [JsonProperty("undefined")]
        public const byte Undefined = 0;

        /// <summary>
        /// Case has a suspended state;
        /// </summary>
        [JsonProperty("suspended")]
        public const byte Suspended = 1;

        /// <summary>
        /// Case has an active state
        /// </summary>
        [JsonProperty("active")]
        public const byte Active = 2;

        /// <summary>
        /// Case has a closed state
        /// </summary>
        [JsonProperty("closed")]
        public const byte Closed = 3;

        public static string GetStatusName(int code)
        {
            switch (code)
            {
                case Active: return nameof(Active).ToLower();
                case Suspended: return nameof(Suspended).ToLower();
                case Closed: return nameof(Closed).ToLower();
                default:
                    return nameof(Undefined).ToLower();
            }
        }
    }

    public static class TitleType
    {
        /// <summary>
        /// Title has undefined state
        /// </summary>
        [JsonProperty("undefined")]
        public const byte Undefined = 0;

        /// <summary>
        /// Designed for keeping records of commercial and private real estate, as well as land
        /// </summary>
        [JsonProperty("immoveable")]
        public const int Immovable = 1;

        /// <summary>
        /// Designed for keeping records of movable / movable property of commercial and private use
        /// </summary>
        [JsonProperty("moveable")]
        public const int Moveable = 2;

        /// <summary>
        /// Designed for keeping records of products made of precious metals, stones, natural resources
        /// </summary>
        [JsonProperty("craft")]
        public const int Craft = 3;

        /// <summary>
        /// Designed for keeping records of raw materials: metals, stones, natural resources
        /// </summary>
        [JsonProperty("resource")]
        public const int Resource = 4;

        public static string GetStatusName(int code)
        {
            switch (code)
            {
                case Immovable: return nameof(Immovable).ToLower();
                case Moveable: return nameof(Moveable).ToLower();
                case Craft: return nameof(Craft).ToLower();
                case Resource: return nameof(Resource).ToLower();
                default:
                    return nameof(Undefined).ToLower();
            }
        }
    }

    public static class ImmoveableTypes
    {
        [JsonProperty("undefined")]
        public const int Undefined = 0;

        [JsonProperty("appartments")]
        public const int Appartments = 1;

        [JsonProperty("business")]
        public const int Business = 2;

        [JsonProperty("storage")]
        public const int Storage = 3;

        [JsonProperty("land")]
        public const int Land = 4;

        public static string GetTypeName(int typeId)
        {
            switch (typeId)
            {
                case Appartments: return nameof(Appartments).ToLower();
                case Business: return nameof(Business).ToLower();
                case Storage: return nameof(Storage).ToLower();
                case Land: return nameof(Land).ToLower();
                default:
                    return nameof(Undefined).ToLower();
            }
        }
    }

    public static class ImmoveableClassType
    {
        private static readonly string[] propertyPotenctial = new string[256];

        static ImmoveableClassType()
        {
            propertyPotenctial[default] = "undefined";
            propertyPotenctial[R1] = "Living area only";
            propertyPotenctial[R2] = "Living or small busines area";
            propertyPotenctial[R3] = "Large Living area";
            propertyPotenctial[R4] = "Non Profit buildings";
            propertyPotenctial[D1] = "Commercial area of level 1";
            propertyPotenctial[D2] = "Commercial area of level 2";
            propertyPotenctial[D3] = "Commercial area of level 3";
            propertyPotenctial[L1] = "Land area for Agreeculture";
            propertyPotenctial[L2] = "Land area for General puproses";
            propertyPotenctial[L3] = "Land area for specific non-dagnerous purposes";
            propertyPotenctial[L4] = "Land area of rads";
            propertyPotenctial[L5] = "Land area of bridges";
            propertyPotenctial[L6] = "Land area for specific dagnerous purposes";
            propertyPotenctial[M1] = "Land area level 1 in Metaverse";
        }

        /// <summary>
        /// Defines Living only area
        /// </summary>
        [JsonProperty("r1")]
        public const int R1 = 1;

        /// <summary>
        /// Defines Living or small bysiness area
        /// </summary>
        [JsonProperty("r2")]
        public const int R2 = 2;

        /// <summary>
        /// Defines large living area
        /// </summary>
        [JsonProperty("r3")]
        public const int R3 = 3;

        /// <summary>
        /// Defines non profit buildings
        /// </summary>
        [JsonProperty("r4")] 
        public const int R4 = 4;

        /// <summary>
        /// Defines commercial area of level 1
        /// </summary>
        [JsonProperty("d1")] 
        public const int D1 = 11;

        /// <summary>
        /// Defines commercial area of level 2
        /// </summary>
        [JsonProperty("d2")]
        public const int D2 = 12;

        /// <summary>
        /// Defines commercial area of level 3
        /// </summary>
        [JsonProperty("d3")] 
        public const int D3 = 13;

        /// <summary>
        /// Defines land area of level 1. Agreculture puproses
        /// </summary>
        [JsonProperty("l1")] 
        public const int L1 = 21;

        /// <summary>
        /// Defines land area of level 2. General puproses
        /// </summary>
        [JsonProperty("l2")] 
        public const int L2 = 22;

        /// <summary>
        /// Defines land area of level 3. Specific non dangerous puproses
        /// </summary>
        [JsonProperty("l3")] 
        public const int L3 = 23;

        /// <summary>
        /// Defines land area of level 4. Area of roads
        /// </summary>
        [JsonProperty("l4")] 
        public const int L4 = 24;

        /// <summary>
        /// Defines land area of level 5. Area of bridges
        /// </summary>
        [JsonProperty("l5")] 
        public const int L5 = 25;

        /// <summary>
        /// Defines land area of level 6. Specific dangerous puproses
        /// </summary>
        [JsonProperty("l6")] 
        public const int L6 = 26;

        /// <summary>
        /// Defines metaverce land area of level 1
        /// </summary>
        [JsonProperty("m1")]
        public const int M1 = 31;

        public static string GetTypeName(int typeId)
        {
            if (typeId < 1 || typeId > propertyPotenctial.Length)
                return propertyPotenctial[0];
            else
            {
                return propertyPotenctial[typeId] ?? propertyPotenctial[0];
            }
        }
    }    
}
