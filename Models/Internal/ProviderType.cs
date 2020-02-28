using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Hyperpack.Models.Internal
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProviderType {
        [EnumMember(Value = "url")]
        Url = 0,
        [EnumMember(Value = "curse")]
        Curse = 1
    }
}