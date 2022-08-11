using System;
using Newtonsoft.Json;

using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Data;
using Delnorte.Shared.Contracts.Annotations;
using Delnorte.Shared.Contracts.Models;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace Delnorte.Terravision.Abstractions.Models
{
    /// <summary>
    /// Represent a main container of asset fields
    /// </summary>
    /// <typeparam name="TPrivate"></typeparam>
    /// <typeparam name="TPublic"></typeparam>
    [RestCollection("certificate", "id", "/certificates")]
    public class Certificate<TPrivate, TPublic> : IIdentified, IIdentified<Guid>, IIdentified<PartitionKey>, IIdentified2, IAssignableTo<Certificate<TPrivate, TPublic>>
        where TPrivate : IIdentified, IIdentified<Guid>, IAssignableTo<TPrivate>, IBinnaryObject<Guid>, new()
        where TPublic : IIdentified, IIdentified<Guid>, IAssignableTo<TPublic>, IBinnaryObject<Guid>, new()
    {
        #region Nested fields

        private class FieldEnumerator : IEnumerator<FieldVisibility>
        {
            private int _index;
            private FieldVisibility? _current = default;
            private readonly List<FieldVisibility> _fields = new List<FieldVisibility>();
            public FieldEnumerator(Certificate<TPrivate, TPublic> cert)
            {
                this.scan(cert, "/", _fields);
            }

            public FieldVisibility Current { get { return _current.Value; } }

            object IEnumerator.Current { get { return this._current; } }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            public bool MoveNext()
            {
                _index++;
                throw new NotImplementedException();
            }

            public void Reset()
            {
                _index = 0;
                _current = default;
            }

            public IEnumerable<FieldVisibility> GetFields(string configuration)
            {
                return _fields.Where(f => f.Configuration == configuration).ToArray();
            }

            ~FieldEnumerator()
            {
                Dispose(false);
            }

            private void Dispose(bool dispManaged)
            {
                _fields.Clear();
            }

            Type ignore = typeof(JsonIgnoreAttribute);
            Type property = typeof(JsonPropertyAttribute);
            Type basic = typeof(BasicFieldAttribute);
            Type extended = typeof(ExtendedFieldAttribute);

            private void scan(object instance, string path, List<FieldVisibility> list)
            {
                foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(instance))
                {
                    if (pd.Attributes[ignore] != null)
                        continue;

                    var prop = pd.Attributes[property] as JsonPropertyAttribute;

                    var fieldName = pd.Name;
                    if (prop != null)
                    {
                        fieldName = prop.PropertyName;
                    }

                    var config = string.Empty;
                    if (pd.Attributes[basic] != null)
                    {
                        config = "basic";
                    }
                    else if (pd.Attributes[extended] != null)
                    {
                        config = "extended";
                    }

                    list.Add(new FieldVisibility(config, fieldName, path));
                    if (Type.GetTypeCode(pd.PropertyType) == TypeCode.Object && fieldName != "SyncRoot")
                    {
                        object value = pd.GetValue(instance);
                        scan(value, path + fieldName, list);
                    }

                }
            }
        }
        #endregion

        private static readonly Type _Type = typeof(Certificate<TPrivate, TPublic>);
        private static object _Configuraton = default;

        public Certificate()
        {
        }

        public Certificate(string ctype
           , decimal dealSize
           , decimal price
           , string currency
           , string status
           , DateTime statusDate
           , DateTime transferDate
           , string signature
           , Guid ownerid
           , string ownerName
           , Guid caseId
           , DocumentRef? content = default
           , TPrivate @private = default
           , TPublic @public = default)
        {
            this.CaseId = caseId;
            this.Price = price;
            this.CertType = ctype;
            this.DealSize = dealSize;
            this.Currency = currency;
            this.Status = status;
            this.StatusDate = statusDate;

            this.Content = content ?? new DocumentRef();

            this.ownerId = new NativeKey<Guid>(ownerid, default);
            this.ownerName = ownerName;
            this.TransferDate = transferDate;
            this.PrivateFields = @private is TPrivate ? @private : new TPrivate();
            this.PublicFields = @public is TPublic ? @public : new TPublic();

            signatureType = eSignatureType.None;
            var arrs = signature.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            char[] chars = default;
            if (arrs.Length > 1)
            {
                signatureType = (eSignatureType)Enum.Parse(typeof(eSignatureType), arrs[0], true);
                chars = arrs[1].ToCharArray();
            }
            else if (arrs.Length > 0)
            {
                chars = arrs[0].ToCharArray();
            }
            else
            {
                chars = new char[0];
            }

            for (int i = 0; i < this.signature.Length && i < chars.Length; i++)
            {
                this.signature[i] = (sbyte)chars[i];
            }
        }

        [JsonConstructor]
        internal Certificate([JsonProperty("c-type")] string ctype
            , [JsonProperty("dealSize")] decimal dealSize
            , [JsonProperty("price")] decimal price
            , [JsonProperty("currencty")] string currency
            , [JsonProperty("status")] string status
            , [JsonProperty("s-date")] DateTime statusDate
            , [JsonProperty("tran-date")] DateTime transferDate
            , [JsonProperty("signature")] string signature
            , [JsonProperty("content")] DocumentRef? content
            , [JsonProperty("caseid")] Guid caseId
            , [JsonProperty("private-fields")] TPrivate @private = default
            , [JsonProperty("public-fields")] TPublic @public = default
            , [JsonProperty("owner-ref")] Reference ownerRef = default)
            : this(ctype, dealSize, price, currency, status, statusDate, transferDate, signature, default, default, caseId, content, @private, @public)
        {
            this.CaseId = caseId;
            if (ownerRef != null)
            {
                ownerName = ownerRef.Title;
                ownerId = new NativeKey<Guid>(Guid.Parse(ownerRef.GetIdentifier().Id), default);
            }
        }

        private eSignatureType signatureType;
        private readonly sbyte[] signature = new sbyte[24];
        private string ownerName;
        private NativeKey<Guid> ownerId;

        [JsonProperty("id")]
        public string Id { get { return (this as IIdentified).Id; } }
        
        /// <summary>
        /// Identifies a Certifiacte type
        /// </summary>
        [JsonProperty("c-type"), BasicField]
        public string CertType { get; set; }

        /// <summary>
        /// Identifies a final price of Certificate
        /// </summary>
        [JsonProperty("dealSize"), BasicField] public decimal DealSize { get; set; }

        /// <summary>
        /// Identifies a basic price of Certificate
        /// </summary>
        [JsonProperty("price"), BasicField] public decimal Price { get; set; }

        /// <summary>
        /// Identifies a price in currecny
        /// </summary>
        /// <remarks>Note that first digit its should ba f(fiat) or c(crypto)</remarks>
        [JsonProperty("currency"), BasicField] public string Currency { get; set; }

        /// <summary>
        /// Identifies a current status code
        /// </summary>
        [JsonProperty("status"), BasicField] public string Status { get; private set; }

        /// <summary>
        /// Identifies Date and Time of Status changing transaction
        /// </summary>
        [JsonProperty("s-date"), BasicField] public DateTime StatusDate { get; private set; }

        /// <summary>
        /// Identifies Date and Time of Owner changing transaction
        /// </summary>
        [JsonProperty("tran-date"), BasicField] public DateTime TransferDate { get; private set; }

        /// <summary>
        /// Stores identifier of wallet or emitter
        /// </summary>
        [BasicField, JsonProperty("signature")]
        public unsafe string Signature
        {
            get
            {
                fixed (sbyte* ptr = &signature[0])
                {
                    return signatureType.ToString().ToLower() + ":" + new string(ptr);
                }
            }
        }

        /// <summary>
        /// Identieis a current Owner address
        /// </summary>
        [JsonProperty("ownerid")] public Guid OwnerId { get { return this.ownerId.Id; } }

        /// <summary>
        /// Identifies active current case reference
        /// </summary>
        [JsonProperty("caseid")] public Guid? CaseId { get; set; }

        /// <summary>
        /// Unique ID that includes a date and hash code of all content materials of this NFT, and person who updated content. 
        /// This code allows to download content materials from off chain servers. This field updating every time content changing
        /// </summary>
        [JsonProperty("content-ref")] public DocumentRef Content { get; private set; }

        /// <summary>
        /// Stores a binnary serialized private certificate fields
        /// </summary>

        [JsonProperty("private-fields")]
        public TPrivate PrivateFields { get; private set; }

        /// <summary>
        /// Stores a binnary serialized public certificate fields
        /// </summary>
        [JsonProperty("public-fields")] public TPublic PublicFields { get; private set; }

        /// <summary>
        /// Identieis a current Owner address
        /// </summary>
        [JsonProperty("owner-ref")]
        private Reference OwnerRef
        {
            get
            {
                var reference = new Reference()
                {
                    Title = ownerName,
                    Type = MembershipProfile.DataType,
                    Url = $"{MembershipProfile.DataType}/{OwnerId.ToString("N")}"
                };

                return reference;
            }
        }

        [JsonProperty("field-visibility")]
        private object FieldVisibility
        {
            get
            {
                if (_Configuraton == default)
                {
                    var enumerator = new FieldEnumerator(this);

                    return new
                    {
                        basic = enumerator.GetFields("basic"),
                        extended = enumerator.GetFields("extended")
                    };
                }

                return _Configuraton;
            }
        }

        public string ExportPublicFields(int serviceId)
        {
            switch (serviceId)
            {
                case 1:
                    return exportToFileIPFS(this);
                default: throw new NotSupportedException();
            }
        }

        public void OpenCase(Guid caseId)
        {
            this.CaseId = caseId;
            this.Status = CertificateStates.GetStatusName(CertificateStates.Suspended);
            this.StatusDate = DateTime.UtcNow;
        }

        public void SetStatus(int statusId)
        {
            this.Status = CertificateStates.GetStatusName(statusId);
            this.StatusDate = DateTime.UtcNow;
            if (statusId == CertificateStates.Active)
            {
                CaseId = default;
            }
        }

        public void SetOwner(Guid ownerId, string ownerName)
        {
            var active = CertificateStates.GetStatusName(CertificateStates.Active);
            Guard.AssertLogic<InvalidOperationException>(() => this.Status == active);
            this.ownerId = new NativeKey<Guid>(ownerId, default);
            this.ownerName = ownerName;
            this.TransferDate = DateTime.UtcNow;
        }

        #region IAssignableTo<Certificate<TPrivate, TPublic>> members
        void IAssignableTo<Certificate<TPrivate, TPublic>>.CopyPropertiesTo(Certificate<TPrivate, TPublic> other, bool makeCopy = false)
        {
            this.signatureType = other.signatureType;
            Array.Clear(other.signature, 0, other.signature.Length);
            this.signature.CopyTo(this.signature, 0);

            other.Price = this.Price;
            other.DealSize = this.DealSize;

            other.CaseId = this.CaseId;
            other.CertType = this.CertType;
            other.Currency = this.Currency;

            other.ownerId = this.ownerId;
            other.ownerName = this.ownerName;
            other.TransferDate = this.TransferDate;

            other.Content = this.Content;

            other.Status = Status;
            other.StatusDate = this.StatusDate;

            if (other.PrivateFields == null && this.PrivateFields != null)
            {
                other.PrivateFields = new TPrivate();
                this.PrivateFields.CopyPropertiesTo(PrivateFields, makeCopy);
            }
            if (other.PrivateFields != null && other.PrivateFields == null)
            {
                other.PrivateFields = default;
            }

            if (other.PublicFields == null && this.PublicFields != null)
            {
                other.PublicFields = new TPublic();
                this.PublicFields.CopyPropertiesTo(PublicFields, makeCopy);
            }
            if (other.PublicFields != null && other.PublicFields == null)
            {
                other.PublicFields = default;
            }
        }
        #endregion

        #region IIdentified<Guid> members
        Guid IIdentified<Guid>.Id { get { return (PrivateFields as IBinnaryObject<Guid>).Id.Id; } }

        ulong IIdentified<Guid>.Revision { get { return (PrivateFields as IBinnaryObject<Guid>).Id.Revision; } }

        bool IEquatable<IIdentified<Guid>>.Equals(IIdentified<Guid> other)
        {
            return other == null ? false : (PrivateFields as IBinnaryObject<Guid>).Id.Equals(other.Id);
        }
        #endregion

        #region IIdentified members
        string IIdentified.Id { get { return (PrivateFields as IBinnaryObject<Guid>).Id.Id.ToString("N"); } }

        ulong IIdentified.Revision { get { return (PrivateFields as IBinnaryObject<Guid>).Id.Revision; } }

        bool IEquatable<IIdentified>.Equals(IIdentified other)
        {
            return other == null ? false : (PrivateFields as IBinnaryObject<Guid>).Id.ToString().Equals(other.Id);
        }
        #endregion

        #region IIdentified<PartitionKey> members

        PartitionKey IIdentified<PartitionKey>.Id
        {
            get
            {
                var id = (PrivateFields as IBinnaryObject<Guid>).Id;

                return new PartitionKey(id.Id.ToString(), id.Id.ToString());
            }
        }

        ulong IIdentified<PartitionKey>.Revision { get { return (PrivateFields as IBinnaryObject<Guid>).Id.Revision; } }       

        bool IEquatable<IIdentified<PartitionKey>>.Equals(IIdentified<PartitionKey> other)
        {
            return other == null ? false : (PrivateFields as IBinnaryObject<Guid>).Id.ToString().Equals(other.Id.Id);
        }

        #endregion

        #region IIdentified2 members

        string IIdentified2.PartitionKey { get { return this.Id; } }

        #endregion

        #region private logic
        private string exportToFileIPFS(Certificate<TPrivate, TPublic> certificate)
        {
            var dict = new Dictionary<string, object>();
            var attributes = new Dictionary<string, object>() {
                { "type", certificate.CertType },
            };

            certificate.PrivateFields.ExportFields(attributes);
            certificate.PublicFields.ExportFields(attributes);

            var exportAttrs = new Dictionary<string, object>();

            if (attributes.ContainsKey("description"))
            {
                dict["description"] = attributes["description"];
                attributes.Remove("description");
            }

            if (attributes.ContainsKey("main-picture"))
            {
                dict["image"] = attributes["main-picture"];
                attributes.Remove("main-picture");
            }

            if (attributes.ContainsKey("name"))
            {
                dict["name"] = attributes["name"];
                attributes.Remove("name");
            }

            foreach (var key in attributes.Keys)
            {
                exportAttrs.Add(key, new
                {
                    trait_type = key,
                    value = attributes[key]
                });
            }
            dict["attributes"] = exportAttrs.Values;
            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }       

        #endregion

    }
}
