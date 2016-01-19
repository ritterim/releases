using System;
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
        public string Email { get; set; }
        public string Logo { get; set; }
        public bool ShowCompanyInHeader { get; set; }
        public bool ShowLogoInHeader { get; set; }
        public bool UseDropdownNavigation { get; set; }

        public IList<GitHubRepository> GetAllRepositories()
        {
            return Repositories
                .Select(x => new GitHubRepository(x.Key, x.Value))
                .ToList();
        }

        public GitHubRepository Find(string id)
        {
            return GetAllRepositories()
                .Where(r => r.FullName.Equals(id, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
        }

        public bool IsMatch(string fullName, string current)
        {
            return fullName.Equals(current, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool HasCompany => !string.IsNullOrEmpty(Company);
        public bool HasEmail => !string.IsNullOrEmpty(Email);
        public bool HasLogo => !string.IsNullOrEmpty(Logo);
    }

    public class GitHubRepository
    {
        public GitHubRepository()
        {
        }

        public GitHubRepository(string fullName, string description)
        {
            var parsedFullName = fullName.Split('/');

            Owner = parsedFullName[0];
            Name = parsedFullName[1];
            Description = description;
            FullName = fullName;
        }

        public string Description { get; protected set; }
        public string Owner { get; protected set; }
        public string Name { get; protected set; }
        public string FullName { get; protected set; }
    }

    public class Stub : GitHubRepository {
        public Stub(string description) {
            Description = description;
        }
    }
}
