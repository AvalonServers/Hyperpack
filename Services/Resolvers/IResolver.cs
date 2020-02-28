using Hyperpack.Models.Internal;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hyperpack.Services.Resolvers
{
    public interface IResolver
    {
        /// <summary>
        /// Resolves all of the specified mods.
        /// </summary>
        public Task<IResolvedMod[]> ResolveAsync(ModExtensionPairs mods, PackPropertiesMinecraft props);
    }
}