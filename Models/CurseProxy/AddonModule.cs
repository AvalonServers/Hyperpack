using Newtonsoft.Json;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonModule
    {
        [JsonProperty("fingerprint")]
        public long Fingerprint;

        [JsonProperty("folderName")]
        public string FolderName;
    }
}