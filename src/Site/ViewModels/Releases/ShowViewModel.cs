using System.Collections.Generic;
using RimDev.Releases.Models;
using System.Linq;

namespace RimDev.Releases.ViewModels.Releases
{
    public class ShowViewModel
    {
        public ShowViewModel(GitHubRepository currentRepository)
        {
            Releases = new List<ReleaseViewModel>();

            CurrentRepository = currentRepository;
        }

        public GitHubRepository CurrentRepository { get; set; }
        public AppSettings AppSettings { get; set; }
        public IList<ReleaseViewModel> Releases { get; set; }
        public bool NotEmpty => Releases != null && Releases.Any();

        public int Page { get; set; }
        public int PageSize { get; set; }

        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public int LastPage { get; set; }

        public int FirstPage { get; set; }
    }


}
