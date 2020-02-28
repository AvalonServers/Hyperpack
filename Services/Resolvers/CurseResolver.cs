using Hyperpack.Models.Internal;
using Hyperpack.Models.Dependency;
using Hyperpack.Models.CurseProxy;
using Hyperpack.Helpers.Exceptions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Hyperpack.Services.Resolvers
{
    /// <summary>
    /// Exposes an interface for resolving CurseForge mods.
    /// </summary>
    public class CurseResolver : IResolver
    {
        private readonly ILogger _logger;
        private readonly CurseApiService _api;
        private const int GAME_ID = 432;
        public CurseResolver(ILoggerFactory logger, CurseApiService api) {
            _logger = logger.CreateLogger<CurseResolver>();
            _api = api;
        }

        public async Task<IResolvedMod[]> ResolveAsync(ModExtensionPairs mods, PackPropertiesMinecraft props)
        {
            var resolved = new Dictionary<int, IResolvedMod>();
            var ids = new ModExtensionPairs();
            var slugs = new ModExtensionPairs();

            foreach (var mod in mods) {
                int modId = 0;
                switch(mod.Key) {
                    case int val:
                        modId = val;
                        break;
                    case string val:
                        int.TryParse(val, out modId);
                        if (modId == 0) {
                            // ensure slug uses proper dash notation
                            slugs.Add(SlugFromName(val), mod.Value);
                            continue;
                        }
                        break;
                    default:
                        throw new ResolverException($"The specified type of mod identifier, {mod.Key.GetType()}, is not implemented.");
                }

                ids.Add(modId, mod.Value);
            }

            await ResolveAsync<int>(ids, resolved, props);
            await ResolveAsync<string>(slugs, resolved, props);
            
            return resolved.Values.ToArray();
        }

        private async Task ResolveAsync<T>(ModExtensionPairs identifiers, IDictionary<int, IResolvedMod> resolved, PackPropertiesMinecraft props) {
            if (identifiers.Count() == 0) return;
            IList<Addon> result;

            _logger.LogDebug("resolving " + string.Join(',', identifiers.Keys));

            // use ids if ident is int, otherwise resolve by slugs
            if (typeof(T) == typeof(int)) {
                result = await _api.GetAddons(new {
                    gameId = GAME_ID,
                    versions = props.Versions,
                    ids = identifiers.Keys
                });
            } else if (typeof(T) == typeof(string)) {
                result = await _api.GetAddons(new {
                    gameId = GAME_ID,
                    versions = props.Versions,
                    slugs = identifiers.Keys
                });
            } else {
                throw new ArgumentException(nameof(identifiers));
            }

            foreach (var addon in result) {
                var tmp = ResolveDefaultFile(addon.Files, props);
                if (!tmp.HasValue) throw new ResolverException($"A suitable file could not be found", addon.Id.ToString());

                // Resolve dependencies
                var file = tmp.Value;
                var depIds = new ModExtensionPairs();
                foreach (var dep in file.Dependencies) {
                    // Don't need to resolve anything we've already resolved
                    if (resolved.ContainsKey(dep.AddonId)) continue;
                    if (depIds.ContainsKey(dep.AddonId)) {
                        _logger.LogWarning($"Duplicated dependency {dep.AddonId} for file {file.Id}, skipping");
                        continue;
                    }

                    depIds.Add(dep.AddonId, null);
                }

                await ResolveAsync<int>(depIds, resolved, props);

                if (resolved.ContainsKey(addon.Id)) {
                    _logger.LogDebug($"dependency {addon.Id} already resolved, skipping");
                    return;
                }

                resolved.Add(addon.Id, new CurseResolvedMod() {
                    AddonId = addon.Id,
                    FileId = file.Id,
                    Url = file.DownloadUrl,
                    Checksum = file.PackageFingerprint
                });
            }

            return;
        }

        // Attempt to find the most up to date version of the specified file
        // this is bound to the Forge and MC version specified.
        private AddonFile? ResolveDefaultFile(AddonFile[] files, PackPropertiesMinecraft props) {
            DateTime? latestVer = null;
            AddonFile? resolved = null;

            foreach (var file in files) {
                if (!file.IsAvailable) continue;
                if (!file.GameVersion.Intersect(props.Versions).Any()) continue;

                // exclude older files
                if (!latestVer.HasValue && latestVer > file.FileDate) continue;

                latestVer = file.FileDate;
                resolved = file;
            }

            return resolved;
        }

        // Converts camel case mod names to slug convention.
        // Example: dynamicSurroundings -> dynamic-surroundings
        private static string SlugFromName(string slug) {
            var sb = new StringBuilder();
            for (int i = 0; i < slug.Length; i++) {
                var value = slug[i];
                if (i != 0 && Char.IsUpper(value)) {
                    sb.Append('-');
                    sb.Append(Char.ToLower(value));
                    continue;
                }

                sb.Append(value);
            }

            return sb.ToString();
        }
    }
}