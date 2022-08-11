
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Delnorte.Shared.Contracts;
using Delnorte.Terravision.Abstractions.Primitives;
using Newtonsoft.Json;

namespace Delnorte.Terravision.Abstractions.Models
{
    /// <summary>
    /// Represents a native object of Conteon Emitter Certificate
    /// </summary>
    public class EmitterCert : IIdentified, IIdentified<Guid>
    {
        public const string DataType = "emitterRef";

        public static readonly Guid ConteonOwner = Guid.Parse("00000001-0001-0001-0001-000000000001");

        public const int idSzie = 16;

        public const int ckeckackSzie = 256;

        public const int SignSize = 2048;

        /// <summary>
        /// Initializes a 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keckack"></param>
        /// <param name="signature"></param>        
        public EmitterCert(string id, string keckack, string signature, DateTime stamp = default)
        {
            unsafe void copyStr2Buffer(string value, sbyte[] buffer, int length)
            {
                var chArr = value.ToCharArray();
                for (int i = 0; i < chArr.Length && i < length; i++)
                {
                    buffer[i] = (sbyte)chArr[i];
                }
            }

            Activated = default;

            Ownership = new Guid2(default, ConteonOwner);

            OwnershipDate = stamp == default(DateTime) ? DateTime.UtcNow : stamp;

            copyStr2Buffer(id, this.id, idSzie);
            copyStr2Buffer(keckack, this.keckack, ckeckackSzie);
            copyStr2Buffer(signature, this.singature, SignSize);
        }

        public EmitterCert(byte[] id, byte[] keckack, byte[] signature, DateTime stamp = default)
        {
            unsafe void copyByte2Buffer(byte[] value, sbyte[] buffer, int length)
            {
                fixed (sbyte* ptr = &buffer[0])
                {
                    var cussor = ptr;
                    for (int i = 0; i < length; i++)
                    {
                        buffer[i] = (sbyte)value[i];
                    }
                }
            }

            Activated = default;

            Ownership = new Guid2(default, ConteonOwner);

            OwnershipDate = stamp == default(DateTime) ? DateTime.UtcNow : stamp;

            copyByte2Buffer(id, this.id, idSzie);
            copyByte2Buffer(keckack, this.keckack, ckeckackSzie);
            copyByte2Buffer(signature, this.singature, SignSize);
        }


        internal EmitterCert(string id, string keckack, string signature, DateTime stamp, Guid distr, Guid owner)
            : this(id, keckack, signature, stamp)
        {
            Ownership = new Guid2(distr, owner);
        }

        [JsonConstructor]
        internal EmitterCert([JsonProperty("id")] string id
            , [JsonProperty("keckack")] string keckack
            , [JsonProperty("signature")] string signature
            , [JsonProperty("ownerdate")] DateTime stamp
            , [JsonProperty("ownership-ref")] Guid2 ownership
            , [JsonProperty("ownername")] string ownerName
            , [JsonProperty("activated")] DateTime activated
            , [JsonProperty("web-site")] string webUrl)
            : this(id, keckack, signature, stamp, ownership.Hi, ownership.Lo)
        {
            this.Activated = activated;
            this.OwnerName = ownerName;
            this.WebSite = webUrl;
        }

        private readonly sbyte[] id = new sbyte[idSzie];
        private readonly sbyte[] keckack = new sbyte[ckeckackSzie];
        private readonly sbyte[] singature = new sbyte[SignSize];

        /// <summary>
        /// Defines an unique emitter identifer
        /// </summary>
        [JsonProperty("id")]
        public string Id { get { return getStringFromArr(this.id); } }

        unsafe private string getStringFromArr(sbyte[] value)
        {
            fixed (sbyte* ptr = &value[0])
            {
                return new string(ptr);
            }
        }

        /// <summary>
        /// Defines working encoding stamp
        /// </summary>
        [JsonProperty("keckack")]
        public string Keckack { get { return getStringFromArr(this.keckack); } }

        /// <summary>
        /// Defines raw firmware signature code
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get { return getStringFromArr(this.singature); } }

        /// <summary>
        /// Identifies and owner ref and assigner
        /// </summary>
        [JsonProperty("ownership-ref")]
        public Guid2 Ownership { get; internal set; }

        /// <summary>
        /// Identifies an ownership date or date of back to distibutor <see cref="IsReplacementMode(EmitterCert)"/>
        /// </summary>
        [JsonProperty("ownerdate")]
        public DateTime OwnershipDate { get; internal set; }

        /// <summary>
        /// Identifies date ant time of first ectivation
        /// </summary>
        [JsonProperty("activated")]
        public DateTime Activated { get; internal set; }

        /// <summary>
        /// Identifies a short owner name
        /// </summary>
        [JsonProperty("ownername")]
        public string OwnerName { get; internal set; }

        /// <summary>
        /// Identifies a owner website or api-data service
        /// </summary>
        [JsonProperty("web-site")]
        public string WebSite { get; set; }

        public void TransferToDistributor(Guid distId)
        {
            OwnerName = default;
            OwnershipDate = DateTime.UtcNow;
            Ownership = new Guid2(distId, ConteonOwner);
        }

        public void AssignOwner(Guid ownerId, string name, string companyUrl)
        {
            //TODO: move to resource file
            Guard.AssertLogic<ValidationException>(() => IsDistributorDefined(this), "Distributor should defined");
            OwnerName = name;
            WebSite = companyUrl;
            OwnershipDate = DateTime.UtcNow;
            Ownership = new Guid2(Ownership.Hi, ownerId);
        }


        #region IIdentified members
        unsafe string IIdentified.Id
        {
            get
            {
                fixed (sbyte* ptr = &id[0])
                {
                    return new string(ptr);
                }
            }
        }

        ulong IIdentified.Revision { get { return default; } }

        bool IEquatable<IIdentified>.Equals(IIdentified other)
        {
            return other == null ? false : (this as IIdentified).Id.Equals(other.Id);
        }
        #endregion

        #region IIdentified<Guid> members
        Guid IIdentified<Guid>.Id
        {
            get
            {
                {
                    unsafe
                    {
                        fixed (void* ptr = &id[0])
                        {
                            return new Guid(new Span<byte>(ptr, idSzie).ToArray());
                        }
                    }
                }
            }
        }

        ulong IIdentified<Guid>.Revision { get { return default; } }

        bool IEquatable<IIdentified<Guid>>.Equals(IIdentified<Guid> other)
        {
            return other == null ? false : (this as IIdentified<Guid>).Id.Equals(other.Id);
        }
        #endregion

        /// <summary>
        /// Receives flag identificating whether emitter cert pushed to repair mode.
        /// </summary>
        /// <param name="val">instance of certificate</param>
        /// <returns>true if emitter in repairable mode</returns>
        /// <remarks>This mode does not allow any operation except archive all related data</remarks>
        public static bool IsReplacementMode(EmitterCert val)
        {
            return val.Ownership.Hi != default && val.Ownership.Lo == ConteonOwner;
        }

        /// <summary>
        /// Receives flag identificating whether emitter cert has only default emitter owner <see cref="ConteonOwner"/>
        /// </summary>
        /// <param name="val">instance of certificate</param>
        /// <returns>true if emitter in default zero mode</returns>
        /// <remarks>This mode doesn't allow any operation with certificate except assign distibutor</remarks>
        public static bool IsZeroMode(EmitterCert val)
        {
            return val.Ownership.Hi == default && val.Ownership.Lo == ConteonOwner;
        }

        /// <summary>
        /// Receives flag identificating whether emitter cert has distributor marker and has no
        /// </summary>
        /// <param name="val">instance of certificate</param>
        /// <returns>true if emitter cert has distributor</returns>
        /// <remarks>This mode doesn't allow any operation with certificate except assign final device owner or re-assign distibutor</remarks>
        public static bool IsDistributorDefined(EmitterCert val)
        {
            return val.Ownership.Hi != default;
        }

        /// <summary>
        /// Receives flag identificating whether emitter cert has distributor marker and final owner identifier which operates with device
        /// </summary>
        /// <param name="val">instance of certificate</param>
        /// <returns>true if emitter cert has distributor</returns>
        /// <remarks></remarks>
        public static bool IsOwnerReady(EmitterCert val)
        {
            return IsDistributorDefined(val) && val.Ownership.Lo != default;
        }

        public static explicit operator NativeKey(EmitterCert val)
        {
            var id = val as IIdentified;
            return new NativeKey(id.Id, id.Revision);
        }

        public static explicit operator Reference(EmitterCert val)
        {
            var reference = new Reference()
            {
                Title = val.OwnerName,
                Type = DataType,
                Url = val.WebSite
            };

            reference.AddProperty("id", val.Id);
            reference.AddProperty("keckack", val.Keckack);
            reference.AddProperty("owner", val.Ownership.Lo.ToString("N"));
            reference.AddProperty("status", getStatus(val));
            reference.AddProperty("uri", val.WebSite);
            return reference;
        }

        public static string getStatus(EmitterCert val)
        {

            if (IsZeroMode(val))
            {
                return CetificateStates.Unassigned;
            }

            if (IsReplacementMode(val))
            {
                return CetificateStates.Retired;
            }

            if (IsDistributorDefined(val) && !IsOwnerReady(val))
            {
                return CetificateStates.Distribution;
            }

            if (IsOwnerReady(val) && val.Activated != default)
            {
                return CetificateStates.Active;
            }

            if (IsOwnerReady(val) && val.Activated == default)
            {
                return CetificateStates.Blocked;
            }

            return CetificateStates.Undefined;
        }
    }

    internal static class CetificateStates
    {

        public const string Undefined = "undefined";

        public const string Active = "active";

        public const string Unassigned = "unassigned";

        public const string Retired = "retired";

        public const string Distribution = "distribution";

        public const string Blocked = "blocked";
    }
}
