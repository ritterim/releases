using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Octokit;
using RimDev.Releases.Models;
using RimDev.Releases.ViewModels.Releases;

namespace Site.Controllers
{
    public class ReleasesController : Controller
    {
        private readonly AppSettings appSettings;
        private readonly GitHubClient gitHub;
        private readonly ILogger logger;

        public ReleasesController(
            IOptions<AppSettings> appSettings,
            GitHubClient gitHub,
            ILogger<ReleasesController> logger)
        {
            this.appSettings = appSettings.Value;
            this.gitHub = gitHub;
            this.logger = logger;
        }
        
        public async Task<IActionResult> Index()
        {
            // take all the repositories
            // Select them all via tasks, using the github client
            // Task.WhenAll()
            // you'd have a ReleaseViewModel

            var model = new IndexViewModel();

            var requests = appSettings
                .GetAllRepositories()
                .Select(async x => await GetLatestRelease(x))
                .ToList();

            foreach (var request in requests) {
                model.Releases.Add(request.Result);
            }

            //var result = await gitHub.Release.GetAll()
            return View(model);
        }  

        public IActionResult Error()
        {
            return View();
        }

        public async Task<ReleaseViewModel> GetLatestRelease(GitHubRepository gitHubRepository) {
            try {
                var releases = await gitHub.Release.GetAll(gitHubRepository.Owner, gitHubRepository.Name);

                return new ReleaseViewModel(gitHubRepository, releases.FirstOrDefault());
            }
            catch (Exception ex)
            {
                logger.LogError($"github request failed for {gitHubRepository.Name}", ex);
                return new ReleaseViewModel(gitHubRepository, null);
            }
        }      
    }
}
