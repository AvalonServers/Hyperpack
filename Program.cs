using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CommandLine;

using Hyperpack.Models.Internal;
using Hyperpack.Services;
using Hyperpack.Services.Providers;
using Hyperpack.Helpers;

namespace Hyperpack
{
    class Program
    {
        private static IServiceProvider provider;

        public static async Task<int> Main(string[] args)
        {
            var parsed = Parser.Default.ParseArguments<BuildArguments, PublishArguments>(args);

            // Load our cache path
            var cachePath = PathHelpers.GetCachePath();
            var cache = new MetaCache(new DirectoryInfo(cachePath));
            await cache.InitAsync();

            // Load injected services
            var collection = new ServiceCollection();
            collection.AddSingleton<CurseApiService>();
            collection.AddSingleton<MetaCache>(cache);
            collection.AddScoped<CurseProvider>();
            collection.AddScoped<UrlProvider>();
            collection.AddScoped<PackService>();
            collection.AddScoped<DependencyResolver>();

            provider = collection.BuildServiceProvider();

            // Parse arguments
            return parsed.MapResult(
                (BuildArguments a) => Build(a).GetAwaiter().GetResult(),
                (PublishArguments a) => Publish(a).GetAwaiter().GetResult(),
                errs => 1
            );
        }

        private static async Task<int> Build(BuildArguments args) {
            var path = PackHelpers.GetPackPath(args.PackDirectory);
            var pack = await PackHelpers.LoadPack(path);
            if (pack == null) return 1;

            var service = provider.GetRequiredService<PackService>();
            service.InitFrom(pack, new DirectoryInfo(path));

            await service.Build();
            return 0;
        }

        private static async Task<int> Publish(PublishArguments args) {
            return 0;
        }
    }

    class BaseArguments {
        [Option('v', "verbose", Required = false, HelpText = "Print verbose output to the console.")]
        public bool Verbose { get; set; }

        [Option('d', "dry-run", Required = false, HelpText = "Run without actually doing anything.")]
        public bool DryRun { get; set; }

        [Option('p', "pack", Required = false, HelpText = "Specifies the path to a pack directory, instead of using the current working directory.")]
        public string PackDirectory { get; set; }
    }

    [Verb("build", HelpText = "Builds the pack.")]
    class BuildArguments : BaseArguments {}

    [Verb("publish", HelpText = "Publishes the pack.")]
    class PublishArguments : BaseArguments {}
}
