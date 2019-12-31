using System.Threading.Tasks;
using System;
using Hyperpack.Models.Internal;
using Hyperpack.Models.Internal.Downloadable;
using Hyperpack.Models.CurseProxy;

namespace Hyperpack.Models.Dependency
{
    public class CurseResolvedMod : IResolvedMod, IHttpDownloadable, IHashed
    {
        public ProviderType Provider => ProviderType.Curse;
        public Guid Identifier { get; private set; } = Guid.NewGuid();

        public int AddonId;
        public int FileId;
        public long Checksum { get; set; }
        public string Url { get; set; }
    }
}