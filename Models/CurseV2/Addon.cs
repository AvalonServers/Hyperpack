using Newtonsoft.Json;

namespace Hyperpack.Models.CurseV2
{
    public struct Addon
    {
        [JsonProperty("attachments")]
        public AddonAttachment[] Attachments;

        [JsonProperty("authors")]
        public AddonAuthor[] Authors;

        [JsonProperty("avatarUrl")]
        public string AvatarUrl;

        [JsonProperty("categories")]
        public AddonCategory[] Categories;

        [JsonProperty("categoryList")]
        public string CategoryList;

        [JsonProperty("categorySection")]
        public AddonCategorySection CategorySection;

        [JsonProperty("clientUrl")]
        public string ClientUrl;

        [JsonProperty("commentCount")]
        public int CommentCount;

        [JsonProperty("donationUrl")]
        public string DonationUrl;

        [JsonProperty("downloadCount")]
        public float DownloadCount;

        [JsonProperty("externalUrl")]
        public string ExternalUrl;

        [JsonProperty("fullDescription")]
        public string FullDescription;

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

        [JsonProperty("installCount")]
        public int InstallCount;

        [JsonProperty("isAvailable")]
        public bool IsAvailable;

        [JsonProperty("isFeatured")]
        public bool IsFeatured;

        [JsonProperty("latestFiles")]
        public File[] LatestFiles;

        [JsonProperty("likes")]
        public int Likes;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("packageType")]
        public int PackageType;

        [JsonProperty("popularityScore")]
        public float PopularityScore;

        [JsonProperty("portalName")]
        public string PortalName;

        [JsonProperty("primaryAuthorName")]
        public string PrimaryAuthorName;

        [JsonProperty("primaryCategoryAvatarUrl")]
        public string PrimaryCategoryAvatarUrl;

        [JsonProperty("primaryCategoryName")]
        public string PrimaryCategoryName;

        [JsonProperty("rating")]
        public int Rating;

        [JsonProperty("sectionName")]
        public string SectionName;

        [JsonProperty("slug")]
        public string Slug;

        [JsonProperty("stage")]
        public int Stage;

        [JsonProperty("status")]
        public int Status;

        [JsonProperty("summary")]
        public string Summary;

        [JsonProperty("websiteUrl")]
        public string WebsiteUrl;
    }
}