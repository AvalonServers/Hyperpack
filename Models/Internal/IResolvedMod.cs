using System;
using Newtonsoft.Json;
using Hyperpack.Helpers;

namespace Hyperpack.Models.Internal
{
    [JsonConverter(typeof(ResolvedModTypeConverter))]
    public interface IResolvedMod
    {
        ProviderType Provider { get; }
        public Guid Identifier { get; }
    }
}