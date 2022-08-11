using System;
using System.IO;
using System.Runtime.InteropServices;
using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Models;
using Delnorte.Terravision.Abstractions.Primitives;
using Newtonsoft.Json;

namespace Delnorte.Terravision.Abstractions.Models
{
    /// <summary>
    /// Represents a native type of Property Case object
    /// </summary>
    /// 
    [StructLayout(LayoutKind.Sequential)]
    public class Case : IIdentified, IIdentified<Guid>, IAssignableTo<Case>
    {
        /// <summary>
        /// Identifies a case identifier
        /// </summary>
        [JsonProperty("key")]
        public NativeKey<Guid> Id { get; set; }

        /// <summary>
        /// Identifies Date and Time of Case is created
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Identieis a Officer address
        /// </summary>
        [JsonProperty("officer-key")]
        public ObjectRef<Guid> Officer { get; set; }

        /// <summary>
        /// Identieis a Facility Contract address or its legal code
        /// </summary>
        [JsonProperty("facility-key")]
        public ObjectRef<Guid> Facility { get; set; }

        /// <summary>
        /// Identifies a case closing Date
        /// </summary>
        [JsonProperty("closed")]
        public DateTime ClosedDate { get; set; }

        /// <summary>
        /// Identifies a reason of case
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Identifies a closing resolution
        /// </summary>
        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        /// <summary>
        /// Gets or sets content reference object
        /// </summary>
        [JsonProperty("content-ref")]
        public DocumentRef Content { get; set; }

        /// <summary>
        /// Identieis a Officer address who closed this case
        /// </summary>
        [JsonProperty("closer-key")]
        public ObjectRef<Guid> Closer { get; set; }

        [JsonProperty("cert-ref")]
        public Guid CertificateRef { get; private set; }
        public Case()
        {     
            Created = DateTime.UtcNow;
            Id = new NativeKey<Guid>(Guid.NewGuid(), default);
        }

        public Case(Guid certRef, ObjectRef<Guid> officer, ObjectRef<Guid> facility, string reason, DateTime stamp = default) :
            this(new NativeKey<Guid>(Guid.NewGuid(), default), certRef, officer, facility, stamp, reason, default, default, null)
        {
        }

        [JsonConstructor]
        internal Case([JsonProperty("key")] NativeKey<Guid> id
            , [JsonProperty("cert-ref")] Guid certRef
            , [JsonProperty("officer-key")] ObjectRef<Guid> officer
            , [JsonProperty("facility-key")] ObjectRef<Guid> facility
            , [JsonProperty("created")] DateTime stamp
            , [JsonProperty("reason")] string reason
            , [JsonProperty("closer-key")] ObjectRef<Guid> closer
            , [JsonProperty("closed")] DateTime closed
            , [JsonProperty("resolution")] string resolution)
        {
            this.Id = id;
            this.CertificateRef = certRef;
            this.ClosedDate = closed;
            this.Closer = officer;
            this.Resolution = resolution;
            this.Officer = officer;
            this.Facility = facility;
            this.Created = stamp == default ? DateTime.UtcNow : stamp;
            this.Reason = reason;
            this.ClosedDate = default;
            this.Resolution = default;
        }

        public void Close(DateTime stamp, string resouliton, ObjectRef<Guid>? officer = null)
        {
            this.ClosedDate = stamp;
            this.Resolution = resouliton;
            this.Closer = Officer;
        }

        #region IIdentified<Guid> region
        Guid IIdentified<Guid>.Id { get { return Id.Id; } }

        ulong IIdentified<Guid>.Revision { get { return Id.Revision; } }

        bool IEquatable<IIdentified<Guid>>.Equals(IIdentified<Guid> other)
        {
            return other == null ? false : this.Id.Id.Equals(other.Id);
        }
        #endregion

        #region IIdentified region
        string IIdentified.Id { get { return Id.Id.ToString("N"); } }

        ulong IIdentified.Revision { get { return Id.Revision; } }

        bool IEquatable<IIdentified>.Equals(IIdentified other)
        {
            return other == null ? false : this.Id.Id.Equals(other.Id);
        }
        #endregion

        #region IAssignableTo<Case> members
        public void CopyPropertiesTo(Case other, bool makeCopy = false)
        {
            other.CertificateRef = this.CertificateRef;
            other.Reason = this.Reason;
            other.Resolution = this.Resolution;
            other.Closer = this.Closer;
            other.ClosedDate = this.ClosedDate;
            other.Created = this.Created;
            other.Facility = this.Facility;
            other.Officer = this.Officer;
            other.Content = this.Content;

            if (makeCopy)
                other.Id = this.Id;
        }
        #endregion
    }
}


