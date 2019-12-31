using Newtonsoft.Json;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonDependency
    {
        [JsonProperty("addonId")]
        public int AddonId;

        [JsonProperty("type")]
        public DependencyType Type;
    }
}