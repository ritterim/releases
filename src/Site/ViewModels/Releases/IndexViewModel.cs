using System.Collections.Generic;
using RimDev.Releases.Infrastructure.GitHub;
using RimDev.Releases.Models;

namespace RimDev.Releases.ViewModels.Releases
{
    public class IndexViewModel
	{
		public IndexViewModel()
		{
            Releases = new List<ReleaseViewModel>();
        }

        public AppSettings AppSettings {get;set;}
		public IList<ReleaseViewModel> Releases {get;set;}
	}

    public class ReleaseViewModel
    {
		public ReleaseViewModel(
			GitHubRepository gitHubRepository,
			Release release)
		{
			GitHubRepository = gitHubRepository;
			Release = release;	
		}
		
		public GitHubRepository GitHubRepository{get;protected set;}
		public Release Release {get;protected set;}

        public string FullName => GitHubRepository.FullName;
        public string Description => GitHubRepository.Description;
        public string Title => Release?.Name;

        public string Body => Release?.Body;
        public string CreatedAt => (Release != null) ? Release.CreatedAt.Date.ToShortDateString() : "n/a";
    }
}