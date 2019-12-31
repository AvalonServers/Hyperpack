using Newtonsoft.Json;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonAuthor
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("projectId")]
        public int ProjectId;

        [JsonProperty("projectTitleId")]
        public int ProjectTitleId;

        [JsonProperty("projectTitleTitle")]
        public string ProjectTitleTitle;

        [JsonProperty("twitchId")]
        public int Twitchid;

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("userId")]
        public int UserId;
    }
}