using System;
using Delnorte.Shared.Contracts;
using Delnorte.Shared.Contracts.Data;
using Delnorte.Terravision.Abstractions.Models;

namespace Delnorte.Terravision.Abstractions.Repository
{
    public interface ICertificateRepository<TPrivate, TPublic> : IReadonlyStorage<Certificate<TPrivate, TPublic>, Guid>
        where TPrivate : IIdentified, IIdentified<Guid>, IAssignableTo<TPrivate>, IBinnaryObject<Guid>, new()
        where TPublic : IIdentified, IIdentified<Guid>, IAssignableTo<TPublic>, IBinnaryObject<Guid>, new()
    {
    }
}
