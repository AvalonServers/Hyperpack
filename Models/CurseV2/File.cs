using Newtonsoft.Json;

namespace Hyperpack.Models.CurseV2
{
    public struct File
    {
        [JsonProperty("alternateFileId")]
        public int AlternateFileId;

        [JsonProperty("dependencies")]
        public AddonDependency[] Dependencies;

        [JsonProperty("downloadUrl")]
        public string DownloadUrl;

        [JsonProperty("fileDate")]
        public string FileDate;
        
        [JsonProperty("fileName")]
        public string FileName;

        [JsonProperty("fileNameOnDisk")]
        public string FileNameOnDisk;

        [JsonProperty("fileStatus")]
        public int FileStatus;

        [JsonProperty("gameVersion")]
        public string[] GameVersion;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("isAlternate")]
        public bool IsAlternate;

        [JsonProperty("isAvailable")]
        public bool IsAvailable;

        [JsonProperty("modules")]
        public AddonModule[] Modules;

        [JsonProperty("packageFingerprint")]
        public long PackageFingerprint;

        [JsonProperty("releaseType")]
        public int ReleaseType;
    }
}