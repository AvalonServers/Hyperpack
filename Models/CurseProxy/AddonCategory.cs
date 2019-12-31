using Newtonsoft.Json;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonCategory
    {
        [JsonProperty("avatarUrl")]
        public string AvatarUrl;

        [JsonProperty("categoryId")]
        public int CategoryId;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("url")]
        public string Url;
    }
}