using Microsoft.Extensions.Logging;

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
    }
}