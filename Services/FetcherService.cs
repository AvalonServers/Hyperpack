using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Hyperpack.Models.Internal;

namespace Hyperpack.Services
{
    /// <summary>
    /// Provides a way to download resolved mods.
    /// </summary>
    public class FetcherService
    {
        private readonly ILogger _logger;
        private readonly CacheService _cache;
        public FetcherService(ILoggerFactory logger, CacheService cache) {
            _logger = logger.CreateLogger<FetcherService>();
            _cache = cache;
        }

        public async Task DownloadAsync(IDownloadableMod mod) {
            
        }

        public async Task DownloadAsync(IEnumerable<IDownloadableMod> mod) {

        }
    }
}