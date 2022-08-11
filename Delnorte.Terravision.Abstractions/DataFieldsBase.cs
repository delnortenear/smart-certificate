using System;
using System.Collections.Generic;
using System.IO;

using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Data;

namespace Delnorte.Terravision.Abstractions.Models
{
    public abstract class DataFieldsBase<T> : IIdentified, IIdentified<T>, IBinnaryObject<T>
        where T : unmanaged
    {
        protected abstract NativeKey<T> ObjectId { get; }

        public abstract void Read(StreamReader r);
        public abstract void Write(StreamWriter r);

        #region IBinnaryReader members
        void IBinnaryReader.Read(StreamReader r)
        {
            this.Read(r);
        }
        #endregion

        #region IBinnaryWriter members
        void IBinnaryWriter.Write(StreamWriter w)
        {
            this.Write(w);
        }

        #endregion IBinnaryObject<Guid>

        #region IBinnaryObject<T> members
        NativeKey<T> IBinnaryObject<T>.Id { get { return ObjectId; } }

        public abstract void ExportFields(Dictionary<string, object> fieldList, params Attribute[] attrs);

        #endregion

        #region IIdentified members
        string IIdentified.Id { get { return this.ObjectId.Id.ToString(); } }

        ulong IIdentified.Revision { get { return this.ObjectId.Revision; } }

        bool IEquatable<IIdentified>.Equals(IIdentified other)
        {
            return other == null ? false : this.ObjectId.Id.Equals(other.Id);
        }
        #endregion

        #region IIdentified<T> members
        T IIdentified<T>.Id { get { return this.ObjectId.Id; } }

        ulong IIdentified<T>.Revision { get { return this.ObjectId.Revision; } }

        bool IEquatable<IIdentified<T>>.Equals(IIdentified<T> other)
        {
            return other == null ? false : this.ObjectId.Id.Equals(other.Id);
        }       
        #endregion
    }

}
