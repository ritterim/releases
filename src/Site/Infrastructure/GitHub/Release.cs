using System;
using System.Collections.Generic;

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

        public string HtmlUrl { get;  set; }

        public string AssetsUrl { get;  set; }

        public string UploadUrl { get;  set; }

        public int Id { get;  set; }

        public string TagName { get;  set; }

        public string TargetCommitish { get;  set; }

        public string Name { get;  set; }

        public string Body { get;  set; }

        public bool Draft { get;  set; }

        public bool Prerelease { get;  set; }

        public DateTimeOffset CreatedAt { get;  set; }

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

        public string AvatarUrl { get;  set; }

        public string Url { get;  set; }

        public string HtmlUrl { get;  set; }

        public string FollowersUrl { get;  set; }

        public string FollowingUrl { get;  set; }

        public string GistsUrl { get;  set; }

        public string StarredUrl { get;  set; }

        public string SubscriptionsUrl { get;  set; }

        public string OrganizationsUrl { get;  set; }

        public string ReposUrl { get;  set; }

        public string EventsUrl { get;  set; }

        public string ReceivedEventsUrl { get;  set; }

        public string Type { get;  set; }

        public bool SiteAdmin { get;  set; }
    }
}

