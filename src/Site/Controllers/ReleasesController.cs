using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using RimDev.Releases.Infrastructure.GitHub;
using RimDev.Releases.Models;
using RimDev.Releases.ViewModels.Releases;

namespace Site.Controllers
{
    public class ReleasesController : Controller
    {
        private readonly AppSettings appSettings;
        private readonly Client gitHub;
        private readonly ILogger logger;

        public ReleasesController(
            AppSettings appSettings,
            Client gitHub,
            ILogger<ReleasesController> logger)
        {
            this.appSettings = appSettings;
            this.gitHub = gitHub;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel();

            var requests = appSettings
                .GetAllRepositories()
                .Select(x => GetLatestRelease(x))
                .ToList();

            await Task.WhenAll(requests);

            foreach (var request in requests)
            {
                model.Releases.Add(request.Result);
            }

            return View(model);
        }

        public async Task<IActionResult> Show(string id)
        {
            var currentRepository = appSettings.Find(id);

            if (currentRepository == null)
                return HttpNotFound();

            var releases = await GetAllReleases(currentRepository);
            var activeRelease = releases.FirstOrDefault(x => x.FullName == id);

            var model = new ShowViewModel(currentRepository);

            foreach (var release in releases)
            {
                model.Releases.Add(release);
            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        private async Task<IEnumerable<ReleaseViewModel>> GetAllReleases(GitHubRepository gitHubRepository)
        {
            try
            {
                var releases = new List<ReleaseViewModel>();
                var gitHubReleases = await gitHub.GetReleases(gitHubRepository.Owner, gitHubRepository.Name);

                foreach (var release in gitHubReleases.Releases)
                {
                    releases.Add(new ReleaseViewModel(gitHubRepository, release));
                }

                return releases;
            }
            catch (Exception ex)
            {
                logger.LogError($"github request failed for {gitHubRepository.Name}", ex);
                return new List<ReleaseViewModel>();
            }
        }

        private async Task<ReleaseViewModel> GetLatestRelease(GitHubRepository gitHubRepository)
        {
            try
            {
                var release = await gitHub.GetLatestRelease(gitHubRepository.Owner, gitHubRepository.Name);
                return new ReleaseViewModel(gitHubRepository, release);
            }
            catch (Exception ex)
            {
                logger.LogError($"github request failed for {gitHubRepository.Name}", ex);
                return new ReleaseViewModel(gitHubRepository, null);
            }
        }
    }
}
