using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Hyperpack.Models.Internal;
using Hyperpack.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Hyperpack.Helpers
{
    internal static class PackHelpers
    {
        internal static string GetPackPath(string dir = null) {
            string folder;

            // Load pack from current or specified directory
            if (!string.IsNullOrWhiteSpace(dir)) {
                folder = dir;
            } else {
                folder = Directory.GetCurrentDirectory();
            }

            return folder;
        }

        internal static async Task<Pack> LoadPack(string dir) {
            var path = Path.Join(dir, "pack.yaml");
            if (string.IsNullOrWhiteSpace(dir) || !File.Exists(path)) {
                Console.WriteLine($"Could not find pack.yaml in {dir}. Please ensure the path to the pack is correct.");
                return null;
            }

            var deserialiser = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

            try {
                var content = await File.ReadAllTextAsync(path);

                using (var reader = new StringReader(content))
                    return deserialiser.Deserialize<Pack>(reader);
            } catch (Exception e) {
                Console.WriteLine("Caught exception while attempting to load and parse the pack configuration file:");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}