using Newtonsoft.Json;

namespace Hyperpack.Models.CurseV2
{
    public struct AddonAuthor
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("url")]
        public string Url;
    }
}