using System;

namespace Hyperpack.Models.Internal
{
    public interface IResolvedMod
    {
        ProviderType Provider { get; }
        public Guid Identifier { get; }
    }
}