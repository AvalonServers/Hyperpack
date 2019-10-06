using Hyperpack.Models.Internal;

namespace Hyperpack.Services.Providers
{
    public interface IProvider
    {
        /// <summary>
        /// Resolves all mods contained in a source group.
        /// </summary>
        public IResolvedMod[] Resolve(Source source);
    }
}