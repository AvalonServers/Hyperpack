using Newtonsoft.Json;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonCategorySection
    {
        [JsonProperty("extraIncludePattern")]
        public string ExtraIncludePattern;

        [JsonProperty("gameId")]
        public int GameId;

        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("initialInclusionPattern")]
        public string InitialInclusionPattern;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("packageType")]
        public PackageType PackageType;

        [JsonProperty("path")]
        public string Path;
    }
}