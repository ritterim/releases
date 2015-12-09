using System.Collections.Generic;
using System.Linq;

namespace RimDev.Releases.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            Repositories = new Dictionary<string, string>();
        }

        public string Company { get; set; }
        public Dictionary<string, string> Repositories { get; set; }
        public string AccessToken { get; set; }

        public IList<GitHubRepository> GetAllRepositories() => Repositories.Select(x => new GitHubRepository(x.Key, x.Value)).ToList();
    }

    public class GitHubRepository
    {
        public GitHubRepository(string fullName, string description)
        {
            var parsedFullName = fullName.Split('/');

            Owner = parsedFullName[0];
            Name = parsedFullName[1];
            Description = description;
        }

        public string Description { get; protected set; }
        public string Owner { get; protected set; }
        public string Name { get; protected set; }
    }
}