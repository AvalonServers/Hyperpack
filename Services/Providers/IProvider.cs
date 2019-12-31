using Hyperpack.Models.Internal;
using System.Threading.Tasks;

namespace Hyperpack.Services.Providers
{
    public interface IProvider
    {
        /// <summary>
        /// Resolves all mods contained in a source group.
        /// </summary>
        public Task<IResolvedMod[]> ResolveAsync(Source source, PackPropertiesMinecraft props);
    }
}