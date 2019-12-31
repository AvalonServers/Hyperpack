using Hyperpack.Models.Internal;
using Hyperpack.Helpers;
using System.Threading.Tasks;
using System.IO;

namespace Hyperpack.Services
{
    /// <summary>
    /// Provides an interface to perform operations on a pack.
    /// </summary>
    public class PackService
    {
        public Pack _pack;
        private DirectoryInfo _folder;

        private readonly MetaCache _cache;
        private readonly DependencyResolver _resolver;

        public PackService(MetaCache cache, DependencyResolver resolver) {
            _cache = cache;
            _resolver = resolver;
        }

        public void InitFrom(Pack pack, DirectoryInfo folder) {
            _pack = pack;
            _folder = folder;
        }

        /// <summary>
        /// Builds the pack by resolving all dependencies.
        /// </summary>
        public async Task Build() {
            // Resolve the mods (and all of their dependencies if possible).
            // This will locate the most up to date version of the mod, and get the identifier of that specific file for the Minecraft version.
            _pack.LockedContent = await _resolver.ResolveAsync(_pack);

            // TODO: Download (and cache)

            // TODO: Generate lock files for packing stage
            // TODO: should resolved also be here???
            await PackHelpers.LockPack(_folder.FullName, _pack);
        }

        /// <summary>
        /// Compiles the pack for distribution.
        /// </summary>
        /// <param name="target">The distribution target to build for.</param>
        public async Task Publish(PackTarget target = PackTarget.All) {
            // TODO: Parse lock files
            // TODO: Generate assets layout
            // TODO: Generate sklauncher zip
            // TODO: Generate server installer
        }

        /// <summary>
        /// Deploys the pack to a web server.
        /// </summary>
        public async Task Deploy() {

        }

        public Pack GetPack() => _pack;
    }
}