using System;

using Newtonsoft.Json;

using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Annotations;
using Delnorte.Shared.Contracts.Data;

namespace Delnorte.Terravision.Abstractions.Models
{
    public abstract class DinamicFieldsBase<T> : DataFieldsBase<T>, IIdentified, IIdentified<T>, IBinnaryObject<T>
        where T : unmanaged
    {
        /// <summary>
        /// Gets or sets object description string
        /// </summary>
        /// <remarks>refers to GENERAL DESCRIPTION OF THE PROPERTY</remarks>
        [JsonProperty("description"), BasicField]
        public string Desciption { get; set; }

        /// <summary>
        /// Gets or sets a base certificate reference
        /// </summary>
        [JsonProperty("certificate-ref"), BasicField]
        public NativeKey<T> CertificateRef { get; set; }

        /// <summary>
        /// Gets or sets reference on owners- proprietor identifier document
        /// </summary>
        /// <example>PROPIETOR´S ID NUMBER</example>
        [JsonProperty("owner-id"), BasicField, NonPublic]
        public ObjectRef<Guid>? PropietorNumber { get; set; }

        /// <summary>
        /// Gets or sets owners phone number
        /// </summary>
        /// <remarks>OWNERS PHONE NUMBER</remarks>
        [JsonProperty("owner-cellnumber"), ExtendedField, NonPublic]
        public string OwnerCellNumber { get; set; }

        /// <summary>
        /// Gets or sets a main picture url
        /// </summary>
        [JsonProperty("main-picture"), BasicField]
        public string PitctureUrl { get; set; }


        protected override NativeKey<T> ObjectId { get { return CertificateRef; } }


    }

}
