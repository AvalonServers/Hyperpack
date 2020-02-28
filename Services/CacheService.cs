using Hyperpack.Models.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Hyperpack.Services
{
    public class CacheService
    {
        private DirectoryInfo _cacheDir;
        private readonly string _cacheFile;
        private IDictionary<string, JObject> _cache;
        public CacheService(DirectoryInfo cacheDir, string cacheFile = "cache.json") {
            _cacheDir = cacheDir;
            _cacheFile = Path.Join(cacheDir.FullName, cacheFile);
        }

        public async Task PutAsync<T>(string key, T value) {
            if (_cache == null) await InitAsync();
            _cache[key] = JObject.FromObject(value);

            await SaveAsync();
        }

        public async Task<T> GetOrDefaultAsync<T>(string key) {
            if (_cache == null) await InitAsync();
            if (!_cache.ContainsKey(key)) return default(T);

            return _cache[key].ToObject<T>();
        }

        public async Task InitAsync() {
            // Create directory
            if (!_cacheDir.Exists)
                _cacheDir.Create();

            if (!File.Exists(_cacheFile)) {
                _cache = new Dictionary<string, JObject>();
                return;
            }

            var data = await File.ReadAllTextAsync(_cacheFile);
            _cache = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(data);
        }

        private async Task SaveAsync() {
            var data = JsonConvert.SerializeObject(_cache);
            await File.WriteAllTextAsync(_cacheFile, data);
        }
    }
}