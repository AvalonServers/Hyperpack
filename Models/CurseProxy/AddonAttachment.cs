using Newtonsoft.Json;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonAttachment
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("isDefault")]
        public bool IsDefault;

        [JsonProperty("projectID")]
        public int ProjectId;

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("url")]
        public string Url;
    }
}