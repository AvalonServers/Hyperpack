using Newtonsoft.Json;

namespace Hyperpack.Models.CurseV2
{
    public struct AddonCategory
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("url")]
        public string Url;
    }
}