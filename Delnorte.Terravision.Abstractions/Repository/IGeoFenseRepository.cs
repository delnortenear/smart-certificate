using System;
using Delnorte.Shared.Contracts;
using Delnorte.Terravision.Abstractions.Primitives;

namespace Delnorte.Terravision.Abstractions.Repository
{
    public interface IGeoFenseRepository : IRepository<GeoFence, Guid>, IReadonlyStorage<GeoFence, Guid>
    {
    }
}
