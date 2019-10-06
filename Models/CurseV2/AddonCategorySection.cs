using Newtonsoft.Json;

namespace Hyperpack.Models.CurseV2
{
    public struct AddonCategorySection
    {
        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("extraIncludePattern")]
        public string ExtraIncludePattern;

        [JsonProperty("gameId")]
        public int GameId;

        [JsonProperty("initialInclusionPattern")]
        public string InitialInclusionPattern;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("packageType")]
        public int PackageType;

        [JsonProperty("path")]
        public string Path;
    }
}