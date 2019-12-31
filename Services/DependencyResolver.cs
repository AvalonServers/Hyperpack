using Hyperpack.Services.Providers;
using Hyperpack.Models.Internal;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

namespace Hyperpack.Services
{
    public class DependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        private static IDictionary<string, Type> ProviderMapper = new Dictionary<string, Type>() {
            { "curse", typeof(CurseProvider) },
            { "url", typeof(UrlProvider) }
        };

        private IDictionary<string, IProvider> Providers;

        public DependencyResolver(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;

            // Register all the provider services in the mapper
            Providers = new Dictionary<string, IProvider>();
            foreach (var provider in ProviderMapper) {
                var service = (IProvider)_serviceProvider.GetRequiredService(provider.Value);
                Providers.Add(provider.Key, service);
            }
        }

        public async Task<IList<IResolvedMod>> ResolveAsync(Pack pack) {
            var list = new List<Source>();

            foreach (var source in pack.Sources) {
                FlattenSources(source, list);
            }

            var resolved = new List<IResolvedMod>();
            foreach (var source in list) {
                // resolve the list of mods
                var mods = await Providers[source.Provider].ResolveAsync(source, pack.Properties.Minecraft);
                resolved.AddRange(mods);
            }

            return resolved;
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