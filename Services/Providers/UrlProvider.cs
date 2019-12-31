using System.Threading.Tasks;
using System.Collections.Generic;
using Hyperpack.Models.Internal;
using Hyperpack.Models.Dependency;

namespace Hyperpack.Services.Providers
{
    public class UrlProvider : IProvider
    {
        public async Task<IResolvedMod[]> ResolveAsync(Source source, PackPropertiesMinecraft props)
        {
            var resolved = new List<IResolvedMod>();
            foreach (var mod in source.Mods) {
                resolved.Add(new UrlResolvedMod() {
                    Url = (string)mod
                });
            }

            return resolved.ToArray();
        }
    }
}