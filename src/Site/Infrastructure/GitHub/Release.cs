using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RimDev.Releases.Infrastructure.GitHub
{
    public class ReleasesResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public ReleasesResponse()
        {
            Releases = new List<Release>();
        }

        public IReadOnlyList<Release> Releases { get; set; }
    }

    public class Release
    {
        public Release() { }

        public Release(string url, string htmlUrl, string assetsUrl, string uploadUrl, int id, string tagName, string targetCommitish, string name, string body, bool draft, bool prerelease, DateTimeOffset createdAt, DateTimeOffset? publishedAt)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            AssetsUrl = assetsUrl;
            UploadUrl = uploadUrl;
            Id = id;
            TagName = tagName;
            TargetCommitish = targetCommitish;
            Name = name;
            Body = body;
            Draft = draft;
            Prerelease = prerelease;
            CreatedAt = createdAt;
            PublishedAt = publishedAt;
        }

        public Release(string uploadUrl)
        {
            UploadUrl = uploadUrl;
        }

        public string Url { get;  set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get;  set; }

        [JsonProperty("assets_url")]
        public string AssetsUrl { get;  set; }

        [JsonProperty("upload_url")]
        public string UploadUrl { get;  set; }

        public int Id { get;  set; }

        [JsonProperty("tag_name")]
        public string TagName { get;  set; }

        [JsonProperty("target_commitish")]
        public string TargetCommitish { get;  set; }

        public string Name { get;  set; }

        public string Body { get;  set; }

        public bool Draft { get;  set; }

        public bool Prerelease { get;  set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get;  set; }

        [JsonProperty("published_at")]
        public DateTimeOffset? PublishedAt { get;  set; }

        public Author Author { get;  set; }
    }

    public class Author
    {
        public Author() { }

        public Author(string login, int id, string avatarUrl, string url, string htmlUrl, string followersUrl, string followingUrl, string gistsUrl, string type, string starredUrl, string subscriptionsUrl, string organizationsUrl, string reposUrl, string eventsUrl, string receivedEventsUrl, bool siteAdmin)
        {
            Login = login;
            Id = id;
            AvatarUrl = avatarUrl;
            Url = url;
            HtmlUrl = htmlUrl;
            FollowersUrl = followersUrl;
            FollowingUrl = followingUrl;
            GistsUrl = gistsUrl;
            Type = type;
            StarredUrl = starredUrl;
            SubscriptionsUrl = subscriptionsUrl;
            OrganizationsUrl = organizationsUrl;
            ReposUrl = reposUrl;
            EventsUrl = eventsUrl;
            ReceivedEventsUrl = receivedEventsUrl;
            SiteAdmin = siteAdmin;
        }

        public string Login { get;  set; }

        public int Id { get;  set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get;  set; }

        public string Url { get;  set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get;  set; }

        [JsonProperty("followers_url")]
        public string FollowersUrl { get;  set; }

        [JsonProperty("following_url")]
        public string FollowingUrl { get;  set; }

        [JsonProperty("gists_url")]
        public string GistsUrl { get;  set; }
        
        [JsonProperty("starred_url")]
        public string StarredUrl { get;  set; }
        
        [JsonProperty("subscriptions_url")]
        public string SubscriptionsUrl { get;  set; }
        
        [JsonProperty("organizations_url")]
        public string OrganizationsUrl { get;  set; }
        
        [JsonProperty("repos_url")]
        public string ReposUrl { get;  set; }
        
        [JsonProperty("events_url")]
        public string EventsUrl { get;  set; }

        [JsonProperty("received_events_url")]
        public string ReceivedEventsUrl { get;  set; }

        public string Type { get;  set; }

        [JsonProperty("site_admin")]
        public bool SiteAdmin { get;  set; }
    }
}

