using RestSharp;
using Newtonsoft.Json;
using Hyperpack.Models.CurseV2;
using Hyperpack.Helpers;
using System.Threading.Tasks;

namespace Hyperpack.Services.Providers
{
    public class CurseApiService
    {
        public const string API_URL = "https://addons-ecs.forgesvc.net/api/v2/";

        private readonly RestClient _client = new RestClient(API_URL);

        public async Task<Addon> GetAddon(string addonId) {
            var request = new RestRequest($"addon/{addonId}");
            var response = await _client.ExecuteTaskAsync(request);
            response.ThrowIfError();

            var addon = JsonConvert.DeserializeObject<Addon>(response.Content);
            return addon;
        }

        public async Task<File[]> GetAddonFiles(string addonId) {
            var request = new RestRequest($"addon/{addonId}/files");
            var response = await _client.ExecuteTaskAsync(request);
            response.ThrowIfError();

            var files = JsonConvert.DeserializeObject<File[]>(response.Content);
            return files;
        }
    }
}