using Hyperpack.Models.Internal;
using Hyperpack.Helpers;
using Hyperpack.Helpers.Exceptions;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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

        private readonly ILogger _logger;
        private readonly CacheService _cache;
        private readonly ResolverService _resolver;

        public PackService(ILoggerFactory logger, CacheService cache, ResolverService resolver) {
            _logger = logger.CreateLogger<PackService>();
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
        public async Task<bool> Build() {
            var failed = false;
            var watch = new Stopwatch();
            watch.Start();

            try {
                // Resolve the mods (and all of their dependencies if possible).
                // This will locate the most up to date version of the mod, and get the identifier of that specific file for the Minecraft version.
                _pack.LockedContent = await _resolver.ResolveAsync(_pack);
                
                watch.Stop();
                _logger.LogInformation($"Taken {watch.ElapsedMilliseconds}ms to resolve {_pack.LockedContent.Count} mods.");
            } catch (ResolverException e) {
                _logger.LogError($"Failed to resolve mod {e}: {e.Message}");
            }

            if (failed) {
                _logger.LogError("Pack compile failed");
                return false;
            }

            // TODO: Download (and cache)

            // TODO: Generate lock files for packing stage
            // TODO: should resolved also be here???
            await PackHelpers.LockPack(_folder.FullName, _pack);
            return true;
        }

        /// <summary>
        /// Compiles the pack for distribution.
        /// </summary>
        /// <param name="target">The distribution target to build for.</param>
        public async Task<bool> Publish(PackTarget target = PackTarget.All) {
            // TODO: Parse lock files
            // TODO: Generate assets layout
            // TODO: Generate sklauncher zip
            // TODO: Generate server installer
            return true;
        }

        /// <summary>
        /// Deploys the pack to a web server.
        /// </summary>
        public async Task<bool> Deploy() {
            return true;
        }

        public Pack GetPack() => _pack;
    }
}