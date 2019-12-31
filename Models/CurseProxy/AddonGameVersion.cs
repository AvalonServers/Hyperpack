using Newtonsoft.Json;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonGameVersion
    {
        [JsonProperty("fileType")]
        public int FileType;

        [JsonProperty("gameVersion")]
        public string GameVersion;

        [JsonProperty("projectFileId")]
        public int ProjectFileId;

        [JsonProperty("projectFileName")]
        public string ProjectFileName;
    }
}