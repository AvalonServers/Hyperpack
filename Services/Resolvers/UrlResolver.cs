using System.Threading.Tasks;
using System.Collections.Generic;
using Hyperpack.Models.Internal;
using Hyperpack.Models.Dependency;
using Hyperpack.Helpers.Exceptions;

namespace Hyperpack.Services.Resolvers
{
    public class UrlResolver : IResolver
    {
        public async Task<IResolvedMod[]> ResolveAsync(ModExtensionPairs mods, PackPropertiesMinecraft props)
        {
            var resolved = new List<IResolvedMod>();
            foreach (var mod in mods) {
                resolved.Add(new UrlResolvedMod() {
                    Url = (string)mod.Key
                });
            }

            return resolved.ToArray();
        }
    }
}