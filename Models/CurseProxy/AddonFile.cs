using Newtonsoft.Json;
using System;

namespace Hyperpack.Models.CurseProxy
{
    public struct AddonFile
    {
        [JsonProperty("alternateFileId")]
        public int AlternateFileId;

        [JsonProperty("dependencies")]
        public AddonDependency[] Dependencies;

        [JsonProperty("downloadUrl")]
        public string DownloadUrl;

        [JsonProperty("fileDate")]
        public DateTime FileDate;

        [JsonProperty("fileLength")]
        public long FileLength;
        
        [JsonProperty("fileName")]
        public string FileName;

        [JsonProperty("fileStatus")]
        public FileStatus FileStatus;

        [JsonProperty("gameVersion")]
        public string[] GameVersion;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("installMetadata")]
        public string InstallMetadata;

        [JsonProperty("isAlternate")]
        public bool IsAlternate;

        [JsonProperty("isAvailable")]
        public bool IsAvailable;

        [JsonProperty("modules")]
        public AddonModule[] Modules;

        [JsonProperty("packageFingerprint")]
        public long PackageFingerprint;

        [JsonProperty("releaseType")]
        public FileType ReleaseType;
    }
}