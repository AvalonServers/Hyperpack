using Hyperpack.Models.Internal;
using Hyperpack.Models.Internal.Downloadable;
using System;

namespace Hyperpack.Models.Dependency
{
    public class UrlResolvedMod : IDownloadableMod
    {
        public ProviderType Provider => ProviderType.Url;
        public Guid Identifier { get; private set; } = Guid.NewGuid();
        public string Url { get; set; }
    }
}