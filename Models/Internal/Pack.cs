using System.Collections.Generic;

namespace Hyperpack.Models.Internal
{
    public class Pack
    {
        public PackProperties Properties;
        public Source[] Sources;
        public IList<IResolvedMod> LockedContent = new List<IResolvedMod>();
    }
}