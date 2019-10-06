using Newtonsoft.Json;

namespace Hyperpack.Models.CurseV2
{
    public struct AddonDependency
    {
        [JsonProperty("addonId")]
        public int AddonId;

        [JsonProperty("type")]
        public int Type;
    }
}