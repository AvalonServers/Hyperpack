using Hyperpack.Models.Internal;
using Hyperpack.Models.Dependency;
using Hyperpack.Models.CurseProxy;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Hyperpack.Services.Providers
{
    /// <summary>
    /// Exposes an interface for resolving CurseForge mods.
    /// </summary>
    public class CurseProvider : IProvider
    {
        private readonly CurseApiService _api;
        private readonly MetaCache _cache;
        public CurseProvider(MetaCache cache, CurseApiService api) {
            _cache = cache;
            _api = api;
        }

        public async Task<IResolvedMod[]> ResolveAsync(Source group, PackPropertiesMinecraft props)
        {
            var resolved = new Dictionary<int, IResolvedMod>();
            var ids = new List<int>();
            var slugs = new List<string>();

            foreach (var mod in group.Mods) {
                dynamic identifier;

                // check if the mod is using extended configuration, and if so use that
                if (mod is Dictionary<dynamic, dynamic> dict) {
                    //var pair = mod as Dictionary<dynamic, dynamic>?;
                    identifier = dict.First().Key;
                } else {
                    identifier = mod;
                }

                int modId = 0;
                switch(identifier) {
                    case int val:
                        modId = val;
                        break;
                    case string val:
                        int.TryParse(val, out modId);
                        if (modId == 0) {
                            // ensure slug uses proper dash notation
                            slugs.Add(SlugFromName(val));
                            continue;
                        }
                        break;
                    default:
                        throw new NotImplementedException($"The specified type of mod identifier, {identifier.GetType()}, is not implemented.");
                }

                ids.Add(modId);
            }

            await ResolveAsync(ids, resolved, props);
            await ResolveAsync(slugs, resolved, props);
            
            return resolved.Values.ToArray();
        }

        private async Task ResolveAsync<T>(IEnumerable<T> identifiers, IDictionary<int, IResolvedMod> resolved, PackPropertiesMinecraft props) {
            if (identifiers.Count() == 0) return;
            IList<Addon> result;

            // query either by ids or slugs
            if (typeof(T) == typeof(int)) {
                result = await _api.GetAddons(new {
                    gameId = 432,
                    versions = props.Versions,
                    ids = identifiers
                });
            } else if (typeof(T) == typeof(string)) {
                result = await _api.GetAddons(new {
                    gameId = 432,
                    versions = props.Versions,
                    slugs = identifiers
                });
            } else {
                throw new ArgumentException(nameof(identifiers));
            }

            foreach (var addon in result) {
                var tmp = ResolveDefaultFile(addon.Files, props);
                if (!tmp.HasValue) throw new Exception($"Unable to resolve a suitable file for addon {addon.Id}");

                // Resolve dependencies
                var file = tmp.Value;
                var depIds = new List<int>();
                foreach (var dep in file.Dependencies) {
                    // Don't need to resolve anything we've already resolved
                    if (resolved.ContainsKey(dep.AddonId)) continue;
                    if (depIds.Contains(dep.AddonId)) {
                        // TODO: log a warning, we should't have duplicate dependencies
                        continue;
                    }

                    depIds.Add(dep.AddonId);
                }

                await ResolveAsync(depIds, resolved, props);

                if (resolved.ContainsKey(addon.Id)) {
                    // TODO: print a warning about duplicate mod
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