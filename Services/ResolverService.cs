using Hyperpack.Services.Resolvers;
using Hyperpack.Models.Internal;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperpack.Services
{
    /// <summary>
    /// Provides a high level interface to resolve mods for a pack.
    /// </summary>
    public class ResolverService
    {
        private readonly IServiceProvider _serviceProvider;

        private static IDictionary<string, Type> ResolverMapping = new Dictionary<string, Type>() {
            { "curse", typeof(CurseResolver) },
            { "url", typeof(UrlResolver) }
        };

        private IDictionary<string, IResolver> Resolvers;

        public ResolverService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;

            // Register all the resolvers in the mapper
            Resolvers = new Dictionary<string, IResolver>();
            foreach (var provider in ResolverMapping) {
                var service = (IResolver)_serviceProvider.GetRequiredService(provider.Value);
                Resolvers.Add(provider.Key, service);
            }
        }

        public async Task<IList<IResolvedMod>> ResolveAsync(Pack pack) {
            var list = new List<Source>();

            foreach (var source in pack.Sources) {
                FlattenSources(source, list);
            }

            var resolved = new List<IResolvedMod>();
            foreach (var source in list) {
                // parse the mods to ID pairs
                var mods = GetModsAsPairs(source);

                // resolve the list of mods
                var rmods = await Resolvers[source.Provider].ResolveAsync(mods, pack.Properties.Minecraft);
                resolved.AddRange(rmods);
            }

            return resolved;
        }

        private ModExtensionPairs GetModsAsPairs(Source source) {
            var pairs = new ModExtensionPairs();

            foreach (var mod in source.Mods) {
                object id;
                Dictionary<string, dynamic> exts = null;

                // check if the mod is using extended configuration, and if so use that
                if (mod is Dictionary<dynamic, dynamic> dict) {
                    var val = dict.First();
                    id = val.Key;
                    exts = val.Value as Dictionary<string, dynamic>;
                } else {
                    id = mod;
                }

                pairs.Add(id, exts);
            }

            return pairs;
        }

        private void FlattenSources(Source source, IList<Source> dest) {
            if (source.Groups != null)
                FlattenSubgroups(source, dest);

            // add the flattened source to the list
            dest.Add(source);
        }

        private void FlattenSubgroups(Source source, IList<Source> dest) {
            foreach (var sub in source.Groups) {
                if (sub is SourceGroup group) {
                    // merge parent attributes with child
                    if (sub.Attributes != null && source.Attributes != null) {
                        foreach (var attrib in source.Attributes) {
                            if (!sub.Attributes.ContainsKey(attrib.Key))
                                sub.Attributes.Add(attrib.Key, attrib.Value);
                        }
                    } else {
                        sub.Attributes = source.Attributes;
                    }

                    if (string.IsNullOrWhiteSpace(sub.Provider)) sub.Provider = source.Provider;

                    // use parent attributes for children if not present
                    if (source is SourceGroup parent) {
                        if (!sub.Selected.HasValue) sub.Selected = parent.Selected;
                        if (!sub.Side.HasValue) sub.Side = parent.Side;
                    }
                }
                    
                FlattenSources(sub, dest);
            }
        }
    }
}