using Delnorte.Shared.Contracts;
using Delnorte.Terravision.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delnorte.Terravision.Abstractions.Repository
{
    public interface IEmitterRepository : IReadonlyStorage<EmitterCert, Guid>
    {
    }
}
