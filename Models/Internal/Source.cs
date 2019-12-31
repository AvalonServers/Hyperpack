using System.Collections.Generic;

namespace Hyperpack.Models.Internal
{
    // This represents an association between an abstract place where mods can be retrieved from and its mods.
    // It can also provide information about that place, such as URLs or authentication tokens.
    public class Source
    {
        public string Provider;
        public Dictionary<string, dynamic> Attributes;
        public dynamic[] Mods;
        public SourceGroup[] Groups;
    }
}