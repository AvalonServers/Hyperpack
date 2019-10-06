using Newtonsoft.Json;

namespace Hyperpack.Models.CurseV2
{
    public struct AddonModule
    {
        [JsonProperty("fingerprint")]
        public long Fingerprint;

        [JsonProperty("folderName")]
        public string FolderName;
    }
}