using System;

using Newtonsoft.Json;

using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Data;
using Delnorte.Shared.Contracts.Annotations;

namespace Delnorte.Terravision.Abstractions.Models
{
    public abstract class CertFieldsBase<T> : DataFieldsBase<T>, IIdentified, IIdentified<T>, IBinnaryObject<T>
        where T : unmanaged
    {
        /// <summary>
        /// Denies an unique property identifer
        /// </summary>
        [BasicField, JsonProperty("key")]
        public NativeKey<T> Id { get; set; }

        /// <summary>
        /// Defines a Properties Name
        /// </summary>
        [BasicField, JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Date of Unique property legal identiifer
        /// </summary>
        [BasicField, JsonProperty("issued-id")]
        public string IssuedNumber { get; set; }

        /// <summary>
        /// Date of Property Creation/Initial registration
        /// </summary>
        [BasicField, JsonProperty("issued-date")]
        public DateTime IssuedDate { get; set; }

        /// <summary>
        /// Defines a title sub type <see cref="ImmoveableTypes"/>
        /// </summary>
        /// <remarks>TYPE OF PROPERTY</remarks>
        [BasicField, JsonProperty("type")]
        public int Type { get; set; }

        /// <summary>
        /// Defines title classicator id. deppends on <see cref="ImmoveableClassType"/>
        /// </summary>
        [JsonProperty("classifier"), BasicField]
        public int Classifier { get; set; }

        protected override NativeKey<T> ObjectId { get { return this.Id; } }
    }

}
