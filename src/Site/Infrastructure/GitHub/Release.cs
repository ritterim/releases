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

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string AssetsUrl { get; protected set; }

        public string UploadUrl { get; protected set; }

        public int Id { get; protected set; }

        public string TagName { get; protected set; }

        public string TargetCommitish { get; protected set; }

        public string Name { get; protected set; }

        public string Body { get; protected set; }

        public bool Draft { get; protected set; }

        public bool Prerelease { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset? PublishedAt { get; protected set; }

        public Author Author { get; protected set; }
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

        public string Login { get; protected set; }

        public int Id { get; protected set; }

        public string AvatarUrl { get; protected set; }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string FollowersUrl { get; protected set; }

        public string FollowingUrl { get; protected set; }

        public string GistsUrl { get; protected set; }

        public string StarredUrl { get; protected set; }

        public string SubscriptionsUrl { get; protected set; }

        public string OrganizationsUrl { get; protected set; }

        public string ReposUrl { get; protected set; }

        public string EventsUrl { get; protected set; }

        public string ReceivedEventsUrl { get; protected set; }

        public string Type { get; protected set; }

        public bool SiteAdmin { get; protected set; }
    }
}

