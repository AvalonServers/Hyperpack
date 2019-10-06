using System;
using System.IO;

namespace Hyperpack.Helpers
{
    public static class PathHelpers
    {
        public static string GetCachePath() {
            switch (Environment.OSVersion.Platform) {
                case PlatformID.Unix:
                    return Path.Join(
                        Environment.GetEnvironmentVariable("HOME"),
                        ".cache/hyperpack"
                    );
                case PlatformID.Win32NT:
                    return Path.Join(
                        Environment.GetEnvironmentVariable("%LOCALAPPDATA%"),
                        "hyperpack"
                    );
                default:
                    throw new NotImplementedException();
            }
        }
    }
}