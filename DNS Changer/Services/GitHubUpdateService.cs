using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DNSChanger.Core.Services
{
    public class GitHubUpdateService : IUpdateService
    {
        private const string ReleasesUrl = "https://api.github.com/repos/AmirSfrz/DNS-Changer---Windows/releases";
        private string _latestReleaseUrl;
        private readonly HttpClient _httpClient;


        public GitHubUpdateService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "DNS-Changer-Windows");
        }

        public async Task<bool> CheckForUpdatesAsync(string currentVersion)
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "DNS-Changer-Windows");

                var response = await httpClient.GetAsync(ReleasesUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var releases = JsonSerializer.Deserialize<GitHubRelease[]>(content);

                if (releases == null || releases.Length == 0)
                    return false;

                var latestRelease = releases[0];
                _latestReleaseUrl = latestRelease.html_url;

                var latestVersion = NormalizeVersion(latestRelease.name);
                var currentNormalized = NormalizeVersion(currentVersion);


                return Version.Parse(latestVersion) > Version.Parse(currentNormalized);
            }
            catch
            {
                // If we can't check for updates, fail silently
                return false;
            }
        }

        public string GetLatestReleaseUrl() => _latestReleaseUrl;

        private string NormalizeVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                return "0.0.0.0";



            // Remove everything after '+' (commit hash)
            int plusIndex = version.IndexOf('+');
            if (plusIndex > 0)
            {
                version = version.Substring(0, plusIndex);
            }


            // Remove 'v' prefix if present
            if (version.StartsWith("v", StringComparison.OrdinalIgnoreCase) || version.StartsWith("V", StringComparison.OrdinalIgnoreCase))
                version = version.Substring(1);

            return version.Trim();
        }

        private class GitHubRelease
        {
            public string tag_name { get; set; }
            public string name { get; set; }
            public string html_url { get; set; }
        }
    }
}