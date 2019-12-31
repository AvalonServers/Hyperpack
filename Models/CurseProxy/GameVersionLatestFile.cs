using Newtonsoft.Json;
using System;

namespace Hyperpack.Models.CurseProxy
{
    public struct GameVersionLatestFile
    {
        [JsonProperty("fileType")]
        public FileType FileType;

        [JsonProperty("gameVersion")]
        public string GameVersion;

        [JsonProperty("projectFileId")]
        public int ProjectFileId;

        [JsonProperty("projectFileName")]
        public string ProjectFileName;
    }
}