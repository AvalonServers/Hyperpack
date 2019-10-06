using Hyperpack.Models.Internal;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hyperpack.Services
{
    public class CacheService
    {
        private readonly DirectoryInfo _cacheDir;
        private ModFile[] _cache;
        public CacheService(DirectoryInfo cacheDir) {
            _cacheDir = cacheDir;
        }

        public async Task Init() {
            // Load cache config file
            if (!_cacheDir.Exists)
                _cacheDir.Create();

            var filePath = Path.Join(_cacheDir.FullName, "cache.json");
            if (!File.Exists(filePath)) {
                _cache = new ModFile[0];
                return;
            }

            var data = await File.ReadAllTextAsync(filePath);
            _cache = JsonConvert.DeserializeObject<ModFile[]>(data);
        }

        public ModFile Resolve() {
            throw new NotImplementedException();
        }
    }
}