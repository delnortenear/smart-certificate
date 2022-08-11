
using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Annotations;
using Delnorte.Shared.Contracts.Data;
using Delnorte.Terravision.Abstractions.Primitives;

namespace Delnorte.Terravision.Abstractions.Models
{
    public class ImmoveableCertFields : CertFieldsBase<Guid>, IIdentified, IIdentified<Guid>, IBinnaryObject<Guid>, IAssignableTo<ImmoveableCertFields>
    {
        /// <summary>
        /// Indculdes reference on Manicipality Name Identifier (Lo) and Manizipality area Identifier(Hi)
        /// </summary>
        /// <remarks>MUNICIPALITY ZONE and MUNICIPALITY</remarks>
        [JsonProperty("manicipality-ref"), ExtendedField, NonPublic]
        public Guid2 ManicipalityRef { get; set; }

        /// <summary>
        /// Defines a property address line, including city and country and basic GPS
        /// </summary>
        /// <remarks>
        /// PROPERTY ADDRESS, COUNTRY, CITY, INDEX
        /// PROPERTY PICTURE
        /// </remarks>
        [JsonProperty("address"), BasicField, NonPublic]
        public Address Address { get; set; }

        /// <summary>
        /// Denies a central Geo position point and sea level of object
        /// </summary>
        /// <remarks>PROPERTY LOCATION(MAP / GPS)</remarks>
        [JsonProperty("location"), BasicField, NonPublic]
        public Gps Gps { get; set; }

        /// <summary>
        /// Defines a construction area 
        /// </summary>
        /// <remarks>TOTAL CONSTRUCTION AREA</remarks>
        [JsonProperty("construct-area"), ExtendedField, NonPublic]
        public double ConstructionArea { get; set; }

        /// <summary>
        /// Defines a living/payload area
        /// </summary>
        /// <remarks></remarks>
        [JsonProperty("payload-area"), BasicField]
        public double PayloadArea { get; set; }

        /// <summary>
        /// Area geometry
        /// </summary>
        /// <remarks>TOTAL PROPERTY AREA</remarks>
        [JsonProperty("total-area"), BasicField]
        public Geometry Area { get; set; }

        /// <summary>
        /// IP PARCEL NUMBER
        /// </summary>
        [JsonProperty("ip-parcelid"), ExtendedField, NonPublic]
        public string IPParcelNumber { get; set; }

        /// <summary>
        /// CNR MAP NUMBER
        /// </summary>
        [JsonProperty("cnr-mapid"), BasicField, NonPublic]
        public string CNRMapNumber { get; set; }

        /// <summary>
        /// IP REGISTRATION NUMBER
        /// </summary>
        [JsonProperty("ip-registryid"), ExtendedField, NonPublic]
        public string IPRregistrationNumber { get; set; }

        #region abstract members implementation
        public override void Read(StreamReader r)
        {
            throw new NotImplementedException();
        }

        public override void Write(StreamWriter r)
        {
            throw new NotImplementedException();
        }

        public override void ExportFields(Dictionary<string, object> fieldList, params Attribute[] attrs)
        {
            Guard.RequiredArgument(fieldList, nameof(fieldList));
            fieldList["name"] = Name;
            fieldList["type"] = ImmoveableTypes.GetTypeName(base.Type);
            fieldList["classifier"] = ImmoveableClassType.GetTypeName(base.Classifier);
            fieldList["payload-area"] = PayloadArea.ToString();
            fieldList["construct-area"] = ConstructionArea.ToString();

            fieldList["address"] = new Address(Address.Country, Address.City, Address.District, null, null, Address.State).GetAddressString();
        }
        #endregion

        #region IAssignableTo<ImmoveableCertFields> members
        public void CopyPropertiesTo(ImmoveableCertFields other, bool makeCopy = false)
        {
        }

        #endregion
    }

    /// <summary>
    /// Represents a part of Immiv
    /// </summary>
    public class ImmoveableDynamicFields : DinamicFieldsBase<Guid>, IIdentified, IIdentified<Guid>, IBinnaryObject<Guid>, IAssignableTo<ImmoveableDynamicFields>
    {
        private readonly char[] currency = new char[4];

        /// <summary>
        /// Defines accomodation amenities flags
        /// </summary>        
        [JsonProperty("amnesties-flags"), ExtendedField]
        public eAmenities AmnetiesFlags { get; set; }

        /// <summary>
        /// Defines a labor assess levels
        /// </summary>
        [JsonProperty("laborFlags"), ExtendedField]
        public eLaborAccess LaborFlags { get; set; }

        /// <summary>
        /// Reference of Geofense zone also describes zone safety level
        /// </summary>
        /// <remarks></remarks>
        [JsonProperty("zone-ref"), ExtendedField]
        public ObjectRef<Guid>? ZoneRef { get; set; }

        /// <summary>
        /// Gets or set object refernece what describes current agree culture activities
        /// </summary>
        /// <remarks>AGRICULTURAL ACTIVITY</remarks>
        [JsonProperty("agreeculture-info"), ExtendedField]
        public ObjectRef<Guid>? AgreeCultureRef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>LAND DEVELOPMENT POTENTIAL</remarks>
        [JsonProperty("land-development-info"), ExtendedField]
        public ObjectRef<Guid>? LandDevelopmentRef { get; set; }

        /// <summary>
        /// Gets or sets reference on terriab description
        /// </summary>
        /// <remarks>TYPE OF TERRAIN</remarks>
        [JsonProperty("terrian-type"), ExtendedField]
        public ObjectRef<Guid>? TypeOfTerrian { get; set; }

        /// <summary>
        /// Reference on Climatic zone
        /// </summary>
        [JsonProperty("climatic-zone"), ExtendedField]
        public ObjectRef<Guid>? ClimaticZoneRef { get; set; }

        [JsonProperty("departament"), ExtendedField, NonPublic]
        public ObjectRef<Guid>? DepartamentRef { get; set; }

        /// <summary>
        /// Gets or sets municipality account file number
        /// </summary>
        /// <remarks>MUNICIPALITY ACCOUNT NUMBER</remarks>
        [JsonProperty("municipality-account"), ExtendedField, NonPublic]
        public ObjectRef<Guid>? MunicipalityFile { get; set; }

        /// <summary>
        /// Gets or sets currency is set by current owner
        /// </summary>
        [JsonProperty("market-currency"), BasicField]
        public string MarketCurrency
        {
            get { return new string(currency).Trim(); }
            set
            {
                Array.Clear(currency, 0, currency.Length);
                if (!string.IsNullOrEmpty(value))
                {
                    var len = value.Length > currency.Length ? currency.Length : value.Length;
                    value.CopyTo(0, currency, 0, len);
                }
            }
        }

        /// <summary>
        /// Gets or sets market price is set by current owner
        /// </summary>
        [JsonProperty("market-price"), BasicField]
        public decimal MarketPrice { get; set; }

        [JsonProperty("basic-labor-access"), ExtendedField]
        public bool? BasicLaborAccess
        {
            get
            {
                if (LaborFlags == eLaborAccess.Undefined)
                    return null;
                else return LaborFlags.HasFlag(eLaborAccess.Basic);
            }
        }

        [JsonProperty("skilled-labor-access"), ExtendedField]
        public bool? SkilledLaborAccess
        {
            get
            {
                if (LaborFlags == eLaborAccess.Undefined)
                    return null;
                else return LaborFlags.HasFlag(eLaborAccess.Skilled);
            }
        }

        [JsonProperty("electricity-access"), ExtendedField]
        public bool? ElectricityAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.Electricity);
            }
        }

        [JsonProperty("running-water-access"), ExtendedField]
        public bool? RunningWaterAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.RunningWater);
            }
        }

        [JsonProperty("mainroad-access"), ExtendedField]
        public bool? MainRoadAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.MainRoad);
            }
        }

        [JsonProperty("cell-access"), ExtendedField]
        public bool? CellPhoneAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.CellPhone);
            }
        }

        [JsonProperty("internet-access"), ExtendedField]
        public bool? InternetAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.Internet);
            }
        }

        [JsonProperty("sewerage-access"), ExtendedField]
        public bool? SewerageAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.Sewerage);
            }
        }

        [JsonProperty("parking-access"), ExtendedField]
        public bool? ParkingAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.ParkingZone);
            }
        }

        [JsonProperty("underground-access"), ExtendedField]
        public bool? UndergroundAccess
        {
            get
            {
                if (AmnetiesFlags == eAmenities.None)
                    return null;
                else return AmnetiesFlags.HasFlag(eAmenities.UnderGround);
            }
        }

        [JsonProperty("distance-measure"), ExtendedField]
        public eDistanceMeasure DistanceMeasure { get; set; }

        [JsonProperty("distance-to-airport"), ExtendedField]
        public uint? DistanceToAirport { get; set; }

        [JsonProperty("distance-to-maincity"), ExtendedField]
        public uint? DistanceToMainCity { get; set; }

        [JsonProperty("distance-to-port"), ExtendedField]
        public uint? DistanceToPort { get; set; }

        [JsonProperty("distance-to-mainroad"), ExtendedField]
        public uint? DistanceToMainRoad { get; set; }

        [JsonProperty("safety-level"), BasicField]
        public byte? SafetyLevel { get; set; }

        /// <summary>
        /// List taxes to be charged
        /// </summary>
        [JsonProperty("taxes-list"), ExtendedField, NonPublic]
        public ObjectRef<Guid>[] AppliedTaxes { get; set; }

        /// <summary>
        /// Gets or set reference on object what described sidewalk state
        /// </summary>
        [JsonProperty("sidewalk-state"), ExtendedField]
        public ObjectRef<Guid>? SideWalk { get; set; }

        /// <summary>
        /// Measurement Descriptor
        /// </summary>
        /// <remarks>refers to MEASUREMENT</remarks>
        [JsonProperty("measurements"), ExtendedField, NonPublic]
        public Guid2? MEASUREMENTS { get; set; }

        #region IAssignableTo<ImmoveableCertFields> members
        public void CopyPropertiesTo(ImmoveableDynamicFields other, bool makeCopy = false)
        {
        }

        #endregion

        #region abstract members implementation
        public override void Read(StreamReader r)
        {
            throw new NotImplementedException();
        }

        public override void Write(StreamWriter r)
        {
            throw new NotImplementedException();
        }

        public override void ExportFields(Dictionary<string, object> fieldList, params Attribute[] attrs)
        {
            string setFlagIfNotNull(bool? value, string trueValue= "yes", string falseValue= "no") {
                if (value.HasValue)
                {
                    if (value.Value)
                    {
                        return trueValue;
                    }
                    else
                        return falseValue;
                }

                return "undefined";
            }

            Guard.RequiredArgument(fieldList, nameof(fieldList));
            fieldList["main-picture"] = PitctureUrl;
            fieldList["description"] = base.Desciption;
           
            if(this.ZoneRef.HasValue)
                fieldList["zone-ref"] = this.ZoneRef.Value.Title;
            if(this.AgreeCultureRef.HasValue)
                fieldList["agreeculture-info"] = this.AgreeCultureRef.Value.Title;

            if(this.LandDevelopmentRef.HasValue)
                fieldList["land-development-info"] = this.LandDevelopmentRef.Value.Title;
            
            if(this.TypeOfTerrian.HasValue)
                fieldList["terrian-type"] = this.TypeOfTerrian.Value.Title;

            fieldList["market-currency"] = this.MarketCurrency;
            fieldList["market-price"] = this.MarketPrice.ToString();
            fieldList["basic-labor-access"] = this.BasicLaborAccess;
            fieldList["basic-labor-access"] = setFlagIfNotNull(this.BasicLaborAccess);
            fieldList["skilled-labor-access"] = setFlagIfNotNull(this.SkilledLaborAccess);
            fieldList["electricity-access"] = setFlagIfNotNull(this.ElectricityAccess);
            fieldList["running-water-access"] = setFlagIfNotNull(this.RunningWaterAccess);
            fieldList["mainroad-access"] = setFlagIfNotNull(this.MainRoadAccess);
            fieldList["cell-access"] = setFlagIfNotNull(this.CellPhoneAccess);
            fieldList["internet-access"] = setFlagIfNotNull(this.InternetAccess);
            fieldList["sewerage-access"] = setFlagIfNotNull(this.SewerageAccess);
            fieldList["parking-access"] = setFlagIfNotNull(this.ParkingAccess);
            fieldList["underground-access"] = setFlagIfNotNull(this.UndergroundAccess);
            fieldList["distance-measure"] = this.DistanceMeasure.ToString();
            fieldList["distance-to-airport"] = this.DistanceToAirport.ToString();
            fieldList["distance-to-maincity"] = this.DistanceToMainCity.ToString();
            fieldList["distance-to-port"] = this.DistanceToPort.ToString();
            fieldList["distance-to-mainroad"] = this.DistanceToMainRoad.ToString();
            fieldList["safety-level"] = this.SafetyLevel.ToString();           
        }
        #endregion

        // PROPIETOR´S NAME
        // 

        // ELECTRICITY PROVIDERS
        // RUNNING WATER PROVIDER
        // INTERNET PROVIDER
        // CELLPHONE PROVIDER

    }
}
