using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Newtonsoft.Json;
using Hyperpack.Models.Internal;
using Hyperpack.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        internal static async Task<Pack> LoadPack(string dir, bool lockfile = false) {
            var filename = lockfile ? "pack.lock" : "pack.yaml";

            var path = Path.Join(dir, filename);
            if (string.IsNullOrWhiteSpace(dir) || !File.Exists(path)) {
                Console.WriteLine($"Could not find {filename} in {dir}. Please ensure the path to the pack is correct.");
                return null;
            }

            try {
                var content = await File.ReadAllTextAsync(path);

                // deserialise JSON if lockfile, else YAML
                if (lockfile) {
                    return JsonConvert.DeserializeObject<Pack>(content);
                } else {
                    var deserialiser = new DeserializerBuilder()
                        .WithNamingConvention(UnderscoredNamingConvention.Instance)
                        .Build();
                    using (var reader = new StringReader(content))
                        return deserialiser.Deserialize<Pack>(reader);
                }
                
            } catch (Exception e) {
                Console.WriteLine("Caught exception while attempting to load and parse the pack configuration file:");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Saves a pack to its corresponding lockfile.
        /// </summary>
        internal static async Task LockPack(string dir, Pack pack) {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            try {
                var str = JsonConvert.SerializeObject(pack, Formatting.Indented);
                await File.WriteAllTextAsync(Path.Join(dir, "pack.lock"), str);
            } catch (Exception e) {
                Console.WriteLine("Caught exception while attempting to save the pack lockfile:");
                Console.WriteLine(e.ToString());
            }
        }
    }
}