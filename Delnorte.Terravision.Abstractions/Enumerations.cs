

using System;

namespace Delnorte.Terravision.Abstractions
{
    public enum eDistanceMeasure: byte
    {
        Miles = 1,
        Kilometers = 2,
    }

    public enum eSignatureType : byte
    {
        None = 0,
        Wallet = 1,
        Emitter = 2
    }

    public enum eLaborAccess : byte {
        Undefined = 0,
        Basic = 1,
        Skilled = 2
    };

    [Flags]
    public enum eAmenities : ulong
    {
        None = 0,
        /// <summary>
        /// RUNNING WATER ACCESS
        /// </summary>
        RunningWater = 1,

        /// <summary>
        /// SEWERAGE ACCESS
        /// </summary>
        Sewerage = 2,

        /// <summary>
        /// ACCESS TO ELECTRICITY
        /// </summary>
        Electricity = 4,

        /// <summary>
        /// ACCESS TO INTERNET
        /// </summary>
        Internet = 8,

        /// <summary>
        /// ACCESSS TO CELLPHONE COVERAGE
        /// </summary>
        CellPhone = 16,

        /// <summary>
        /// ACCESS TO MAIN ROAD
        /// </summary>
        MainRoad = 32,

        /// <summary>
        /// ACCESS TO Parking Zone
        /// </summary>
        ParkingZone = 64,

        /// <summary>
        /// ASSESS to Central heating
        /// </summary>
        CentralHeating = 128,

        /// <summary>
        /// ASSESS TO Air condition
        /// </summary>
        AirCondition = 256,

        /// <summary>
        /// ACCESS to underground area
        /// </summary>
        UnderGround = 512
    }

    [Flags]
    public enum eInfrastructure: ulong {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Access to Heavy vecile and transport parking
        /// </summary>
        HeaviVecileParking = 1,

        /// <summary>
        /// PROXIMITY TO HOSPITALS
        /// </summary>
        Hospitals = 64,

        /// <summary>
        /// PROXIMITY SCHOOL & UNIVERSITIES 
        /// </summary>
        Education = 128,
    }
}
