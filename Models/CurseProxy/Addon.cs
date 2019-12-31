using Newtonsoft.Json;
using System;

namespace Hyperpack.Models.CurseProxy
{
    public struct Addon
    {
        [JsonProperty("attachments")]
        public AddonAttachment[] Attachments;

        [JsonProperty("authors")]
        public AddonAuthor[] Authors;

        [JsonProperty("categories")]
        public AddonCategory[] Categories;

        [JsonProperty("categorySection")]
        public AddonCategorySection CategorySection;

        [JsonProperty("dateCreated")]
        public DateTime DateCreated;

        [JsonProperty("dateModified")]
        public DateTime DateModified;

        [JsonProperty("dateReleased")]
        public DateTime DateReleased;

        [JsonProperty("defaultFileId")]
        public int DefaultFileId;

        [JsonProperty("downloadCount")]
        public int DownloadCount;

        [JsonProperty("gameId")]
        public int GameId;

        [JsonProperty("gameName")]
        public string GameName;

        [JsonProperty("gamePopularityRank")]
        public int GamePopularityRank;

        [JsonProperty("gameVersionLatestFiles")]
        public AddonGameVersion[] GameVersionLatestFiles;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("isAvailable")]
        public bool IsAvailable;

        [JsonProperty("isFeatured")]
        public bool IsFeatured;

        [JsonProperty("latestFiles")]
        public GameVersionLatestFile[] LatestFiles;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("packageType")]
        public int PackageType;

        [JsonProperty("popularityScore")]
        public float PopularityScore;

        [JsonProperty("portalName")]
        public string PortalName;

        [JsonProperty("primaryLanguage")]
        public string PrimaryLanguage;

        [JsonProperty("slug")]
        public string Slug;

        [JsonProperty("status")]
        public ProjectStatus Status;

        [JsonProperty("summary")]
        public string Summary;

        [JsonProperty("websiteUrl")]
        public string WebsiteUrl;

        [JsonProperty("files")]
        public AddonFile[] Files;
    }
}