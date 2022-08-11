using Delnorte.Shared.Contracts;

using System;
using System.IO;
using System.Collections.Generic;

using Delnorte.Terravision.Abstractions.Models;
using Delnorte.Terravision.Abstractions.Primitives;
using Delnorte.Shared.Contracts.Models;

namespace Delnorte.Terravision.Abstractions.Repository
{
    public interface ICaseRepository: IReadonlyStorage<Case, Guid>
    {
        /// <summary>
        /// Creates a new case for certificate by Id
        /// </summary>
        /// <param name="certId">Certificate Identifier</param>
        /// <param name="facility">Faciliated admin structure is perforing action</param>
        /// <param name="officier">Id of Officier is performing action</param>
        /// <param name="reason">Reason of creation case</param>
        /// <param name="stamp">Current time stamp</param>
        /// <returns>Identifier of Generated case</returns>
        IIdentified<Guid> Create(IIdentified<Guid> certId, IIdentified<Guid> facility, IIdentified<Guid> officier, string reason, DateTime? stamp, DocumentRef? content);

        /// <summary>
        /// Updates content attached for case
        /// </summary>
        /// <param name="caseId"></param>
        /// <param name="contentRef"></param>
        void UpdateCase(IIdentified<Guid> caseId, DocumentRef contentRef);

        /// <summary>
        /// Closes case by ID
        /// </summary>
        /// <param name="id">case identifier</param>
        /// <param name="closer">Identifier of officier is perforiming action</param>
        /// <param name="resolution">Final resolution</param>
        void Close(IIdentified<Guid> id, Guid closer, string resolution);

        /// <summary>
        /// Receives all cases related to certificate
        /// </summary>
        /// <param name="certificate">Unique key of certificate</param>
        /// <returns>Array of references on Case</returns>
        IEnumerable<IReference> CaseHistory(IIdentified<Guid> certificate);
    }
}
