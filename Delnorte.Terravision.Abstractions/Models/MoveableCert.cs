using System;
using System.IO;

using Newtonsoft.Json;

using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Annotations;
using Delnorte.Shared.Contracts.Data;
using System.Collections.Generic;

namespace Delnorte.Terravision.Abstractions.Models
{
    public class MoveableCertFields : CertFieldsBase<Guid>, IIdentified, IIdentified<Guid>, IBinnaryObject<Guid>, IAssignableTo<MoveableCertFields>
    {
        #region IAssignableTo<MoveableCertFields> members
        public void CopyPropertiesTo(MoveableCertFields other, bool makeCopy = false)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region abstract members implementation
        public override void Read(StreamReader r)
        {
        }

        public override void Write(StreamWriter r)
        {
        }

        public override void ExportFields(Dictionary<string, object> fieldList, params Attribute[] attrs)
        {
            
        }
        #endregion
    }

    public class MoveableDynamicFields : DinamicFieldsBase<Guid>, IIdentified, IIdentified<Guid>, IBinnaryObject<Guid>, IAssignableTo<MoveableDynamicFields>
    {
        [BasicField, JsonProperty("color")]
        public string Color { get; set; }

        [ExtendedField, JsonProperty("vin")]
        public string VIN { get; set; }
        
        #region IAssignableTo<MoveableCertFields> members
        public void CopyPropertiesTo(MoveableDynamicFields other, bool makeCopy = false)
        {
            other.CertificateRef = this.CertificateRef;
            other.Color = this.Color;
            other.VIN = this.VIN;
        }

        #endregion


        #region abstract members implementation
        public override void Read(StreamReader r)
        {
        }

        public override void Write(StreamWriter r)
        {
        }

        public override void ExportFields(Dictionary<string, object> fieldList, params Attribute[] attrs)
        {

        }
        #endregion
    }
}
