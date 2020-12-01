using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hyperpack.Models.CurseProxy
{
    public struct GetAddonsResponse
    {
        [JsonProperty("addons")]
        public List<Addon> Addons;
    }
}