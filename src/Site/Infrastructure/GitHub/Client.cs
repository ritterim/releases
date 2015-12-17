using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RimDev.Releases.Infrastructure.GitHub
{
    public class Client
    {
        private readonly string apiToken;
        private const string baseUrl = "https://api.github.com/";
        private readonly string userAgent;
        public const string DefaultUserAgent = "RimDev.Releases";

        public Client(string apiToken, string userAgent = DefaultUserAgent)
        {
            this.apiToken = apiToken;
            this.userAgent = userAgent;
        }

        public async Task<ReleasesResponse> GetReleases(string owner, string repo, int page = 1, int pageSize = 10)
        {
            var url = new Uri($"{baseUrl}repos/{owner}/{repo}/releases?page={page}&per_page={pageSize}");

            using (var client = GetClient(url))
            {
                var result = await client.GetAsync("");

                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadAsStringAsync();

                IEnumerable<string> links;
                result.Headers.TryGetValues("Link", out links);
                var header = (links ?? new string[0]).FirstOrDefault();
                Console.WriteLine($"header : {header}");

                return new ReleasesResponse
                {
                    Releases = JsonConvert.DeserializeObject<List<Release>>(response).AsReadOnly(),
                    Page = page,
                    PageSize = pageSize
                }.ParsePaging(header);
            }
        }

        public async Task<Release> GetLatestRelease(string owner, string repo)
        {
            var release = await GetReleases(owner, repo, 1, 1);
            return release.Releases.FirstOrDefault();
        }

        public async Task<string> RenderMarkdown(string markdown, string context, string mode = "gfm")
        {
            var url = new Uri($"{baseUrl}markdown");
            var json = new { text = markdown, mode = mode, context = context };

            using (var client = GetClient(url))
            {
                var response = await client.PostAsync("", new StringContent(JsonConvert.SerializeObject(json)));
                return await response.Content.ReadAsStringAsync();
            }
        }

        private HttpClient GetClient(Uri uri)
        {
            var client = new HttpClient
            {
                BaseAddress = uri
            };

            client.DefaultRequestHeaders.Add("Authorization", $"Token {apiToken}");
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);

            return client;
        }
    }
}
