using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommandLine;

using Hyperpack.Models.Internal;
using Hyperpack.Services;
using Hyperpack.Services.Resolvers;
using Hyperpack.Helpers;

namespace Hyperpack
{
    class Program
    {
        private static IServiceProvider provider;

        public static async Task<int> Main(string[] args)
        {
            var result = Parser.Default.ParseArguments(args, new Type[] { 
                typeof(BuildArguments), 
                typeof(PublishArguments) 
            });

            BaseArguments baseArgs;
            if (result.Tag == ParserResultType.Parsed) {
                var parsed = result as Parsed<object>;
                baseArgs = (BaseArguments) parsed.Value;
            } else {
                // print errors
                return 1;
            }

            // Load our cache path
            var cachePath = PathHelpers.GetCachePath();
            var cache = new CacheService(new DirectoryInfo(cachePath));
            await cache.InitAsync();

            // Load injected services
            var collection = new ServiceCollection();
            collection.AddSingleton<CurseApiService>();
            collection.AddSingleton<CacheService>(cache);
            collection.AddScoped<CurseResolver>();
            collection.AddScoped<UrlResolver>();
            collection.AddScoped<PackService>();
            collection.AddScoped<ResolverService>();
            collection.AddScoped<FetcherService>();
            collection.AddLogging(logging => {
                logging.AddConsole();

                if (baseArgs.Verbose) {
                    logging.SetMinimumLevel(LogLevel.Debug);
                } else {
                    logging.SetMinimumLevel(LogLevel.Information);
                }
            });

            provider = collection.BuildServiceProvider();

            // Parse arguments
            return result.MapResult(
                (BuildArguments a) => Build(a).GetAwaiter().GetResult(),
                (PublishArguments a) => Publish(a).GetAwaiter().GetResult(),
                errs => 1
            );
        }

        private static async Task<int> Build(BuildArguments args) {
            var pack = await LoadPack(args.PackDirectory);
            if (pack == null) return 1;

            return await pack.Build() ? 0 : 1;
        }

        private static async Task<int> Publish(PublishArguments args) {
            var pack = await LoadPack(args.PackDirectory, true);
            if (pack == null) return 1;

            return await pack.Publish() ? 0 : 1;
        }

        private static async Task<PackService> LoadPack(string dir, bool lockfile = false) {
            var path = PackHelpers.GetPackPath(dir);
            var pack = await PackHelpers.LoadPack(path, lockfile);
            if (pack == null) return null;

            var service = provider.GetRequiredService<PackService>();
            service.InitFrom(pack, new DirectoryInfo(path));
            return service;
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
