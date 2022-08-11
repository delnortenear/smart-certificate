using System;
using System.Collections.Generic;
using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Models;
using Delnorte.Terravision.Abstractions.Models;
using Newtonsoft.Json;

namespace Delnorte.Terravision.Abstractions
{
    /// <summary>
    /// Represents an inteface of extractor private fields from smart certificate
    /// </summary>
    public interface ISmartCertificateAccessor
    {
        /// <summary>
        /// Smart Certificate identifier
        /// </summary>
        IIdentified<Guid> CertificateId { get; }

        /// <summary>
        /// Unique Identifier of accessor
        /// </summary>
        ObjectRef<Guid> Accessor { get; }

        /// <summary>
        /// Gets a flag indicating whether database should be synced with certficate fields
        /// </summary>
        bool ShouldSync { get; }

        /// <summary>
        /// Transaction signature is used to generate permit
        /// </summary>
        string TranSignature { get; }


        /// <summary>
        /// Receives a permission record for a content into storage
        /// </summary>
        /// <returns></returns>
        DocPermission? Permit();

        void Block();

        /// <summary>
        /// Reverts owner metadata in cerificate and receives a list of original permits
        /// </summary>
        /// <returns></returns>
        /// <remarks>This action could be reverted by acceptar role or current owner</remarks>
        IEnumerable<DocPermission> Revert();
    }

    public struct Connection {

        [JsonProperty("encyption")]
        public string EncriptionKey;

        [JsonProperty("connection")]
        public string DbConnection;

        [JsonProperty("stamp")]
        public long ModifyStamp;
    }

    public struct Accessor
    {
        [JsonConstructor]
        public Accessor([JsonProperty("encyption")] string encHash
            , [JsonProperty("connection")] string dbConnection
            , [JsonProperty("has-read")] bool hasRead
            , [JsonProperty("has-write")] bool hasWrite
            , [JsonProperty("has-owner")] bool hasOwner = false
            )
        {
            this.AllowRead = hasRead;
            this.AllowWrite = hasWrite;
            this.OwnerAccess = hasOwner;
            this.EncriptionKey = encHash;
            this.DbConnection = dbConnection;
        }

        [JsonProperty("has-owner")]
        public bool OwnerAccess;

        [JsonProperty("has-read")]
        public bool AllowRead;

        [JsonProperty("has-write")]
        public bool AllowWrite;

        [JsonProperty("encyption")]
        public string EncriptionKey;

        [JsonProperty("connection")]
        public string DbConnection;
    }
}
