using RestSharp;
using System;

namespace Hyperpack.Helpers
{
    public static class RestExtensions
    {
        public static void ThrowIfError(this IRestResponse response) {
            if (!response.IsSuccessful)
                throw new Exception($"API returned code {response.StatusCode} ({response.StatusDescription}).");
        }
    }
}