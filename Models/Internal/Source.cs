using System.Collections.Generic;

namespace Hyperpack.Models.Internal
{
    public class Source
    {
        public string Provider;
        public Dictionary<string, dynamic> Attributes;
        public dynamic[] Mods;
        public SourceGroup[] Groups;
    }
}