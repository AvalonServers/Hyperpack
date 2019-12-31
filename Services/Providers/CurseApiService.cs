using RestSharp;
using GraphQL.Client;
using GraphQL.Common.Request;
using GraphQL.Common.Response;
using Newtonsoft.Json;
using Hyperpack.Models.Internal;
using Hyperpack.Models.CurseProxy;
using Hyperpack.Helpers;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Collections.Generic;

namespace Hyperpack.Services.Providers
{
    public class CurseApiService
    {
        public const string PROXY_URL = "https://curse.nikky.moe";
        public const string CURSEFORGE_URL = "https://minecraft.curseforge.com/projects";

        public const string USER_AGENT = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:70.0) Gecko/20100101 Firefox/70.0";
        private readonly GraphQLClient _graphQl = new GraphQLClient($"{PROXY_URL}/graphql");
        
        public async Task<IList<Addon>> GetAddons(object variables) {
            var request = new GraphQLRequest {
                Query = @"
                    query (
                        $gameId: Int,
                        $versions: [String!],
                        $ids: [Int!],
                        $slugs: [String!],
                        $section: String!,
                        $releaseTypes: [FileType!]){
                        addons(gameId: $gameId, gameVersionList: $versions, idList: $ids, slugList: $slugs, section: $section) {
                            authors() {
                                name
                                url
                            }
                            id
                            isAvailable
                            name
                            portalName
                            primaryLanguage
                            slug
                            status
                            websiteUrl
                            files() {
                                dependencies() {
                                    addonId
                                    type
                                }
                                downloadUrl
                                fileDate
                                fileStatus
                                gameVersion
                                isAvailable
                                packageFingerprint
                                releaseType
                            }
                        }
                    }
                ",
                Variables = variables
            };

            try {
                var response = await _graphQl.PostAsync(request);
                var data = response.GetDataFieldAs<List<Addon>>("addons");

                return data;
            } catch (Exception e) {
                throw e;
            }
        }
    }
}