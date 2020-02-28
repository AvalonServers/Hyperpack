using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hyperpack.Models.Internal;
using Hyperpack.Models.Dependency;

namespace Hyperpack.Helpers
{
    internal class ResolvedModTypeConverter : JsonConverter<IResolvedMod> {
        public override IResolvedMod ReadJson(JsonReader reader, Type objectType, IResolvedMod existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // deserialize based on Provider enum
            var jsonObject = JObject.Load(reader);
            IResolvedMod resolved = default(IResolvedMod);

            var type = jsonObject.Value<string>("Provider");
            var provider = (ProviderType) Enum.Parse(typeof(ProviderType), type, true);

            switch (provider)
            {
                case ProviderType.Url:
                    resolved = new UrlResolvedMod();
                    break;
                case ProviderType.Curse:
                    resolved = new CurseResolvedMod();
                    break;
            }

            serializer.Populate(jsonObject.CreateReader(), resolved);
            return resolved;
        }

        public override void WriteJson(JsonWriter writer, IResolvedMod value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
}