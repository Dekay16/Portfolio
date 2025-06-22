using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;
using Portfolio.Business.ViewModels;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectsManager _manager;

        public HomeController(ILogger<HomeController> logger, IProjectsManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public IActionResult Index()
        {
            var vm = _manager.GetAllProjects();

            //var test = new ProjectsViewModel
            //{
            //    Title = "Test Title",
            //    Description = "Test Desc",
            //    Technologies = "Test Tech",
            //    GitHubLink = "google.com",
            //    Extra = "Test Extra"
            //};
            //_manager.AddProject(test);

            return View(vm);
        }

        public IActionResult AddEditProject()
        {
            var vm = new ProjectsViewModel();
            return PartialView("/Partial/_AddEditProject", vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
