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

            return View(vm);
        }

        [HttpGet]
        public IActionResult AddEditProject(int? id)
        {
            var vm = new ProjectsViewModel();
            if(id.HasValue && id != 0)
            {
                vm = _manager.GetProjectById(id.Value);
            }
            else
            {
                vm = new ProjectsViewModel();
            }

            return PartialView("Partial/_AddEditProject", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditProject(ProjectsViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("Partial/_AddEditProject", vm);
            }

            if(vm.ID == 0 || vm.ID == null)
            {
                var model = new ProjectsViewModel
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    Technologies = vm.Technologies,
                    GitHubLink = vm.GitHubLink,
                    Extra = vm.Extra
                };

                _manager.AddProject(model);

            }
            else
            {
                _manager.EditProject(vm);
            }

            return Json(new { success = true });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
