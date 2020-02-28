using System;

namespace Hyperpack.Helpers.Exceptions
{
    public class ResolverException : Exception
    {
        public string ModId;
        internal ResolverException(string message, string modId = null) : base(message) {
            ModId = modId;
        }
    }
}