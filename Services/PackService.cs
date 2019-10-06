using Hyperpack.Models.Internal;
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

        private readonly CacheService _cache;
        private readonly DependencyResolver _resolver;

        public PackService(CacheService cache, DependencyResolver resolver) {
            _cache = cache;
            _resolver = resolver;
        }

        public void InitFrom(Pack pack, DirectoryInfo folder) {
            _pack = pack;
        }

        /// <summary>
        /// Builds the pack by resolving all dependencies.
        /// </summary>
        public async Task Build() {
            // TODO: Resolve dependencies (transform to provider's notation & prepare for download)
            _resolver.Resolve(_pack);

            // TODO: Download (and cache)

            // TOOD: Generate lock files for packing stage
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