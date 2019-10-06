using Hyperpack.Models.Internal;

namespace Hyperpack.Services.Providers
{
    /// <summary>
    /// Exposes an interface for retrieving mods and mod metadata from CurseForge.
    /// </summary>
    public class CurseProvider : IProvider
    {
        public IResolvedMod[] Resolve(Source group)
        {
            foreach (var mod in group.Mods) {
                
            }
        }
    }
}