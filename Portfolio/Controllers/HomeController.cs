using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;
using Portfolio.Business.Managers;
using Portfolio.Business.ViewModels;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IErrorLogger _logger;
        private readonly IProjectsManager _manager;

        public HomeController(IErrorLogger logger, IProjectsManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        public IActionResult Index()
        {
            try
            {
                var vm = _manager.GetAllProjects();
                return View(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.Index");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet]
        public IActionResult AddEditProject(int? id)
        {
            try
            {
                var vm = new ProjectsViewModel();
                if (id.HasValue && id != 0)
                {
                    vm = _manager.GetProjectById(id.Value);
                }
                else
                {
                    vm = new ProjectsViewModel();
                }

                return PartialView("Partial/_AddEditProject", vm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro in HomeController.AddEditProject - GET");
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditProject([FromForm] ProjectsViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("Partial/_AddEditProject", vm);
                }

                if (vm.ID == 0 || vm.ID == null)
                {
                    var model = new ProjectsViewModel
                    {
                        Title = vm.Title,
                        Description = vm.Description,
                        Technologies = vm.Technologies,
                        GitHubLink = vm.GitHubLink,
                        Extra = vm.Extra,
                        ImageFile = vm.ImageFile
                    };

                    _manager.AddProject(model);
                }
                else
                {
                    _manager.EditProject(vm);
                }

                // Return JSON so client-side can detect success and close the modal
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro in HomeController.AddEditProject - POST");
                throw;
            }
        }

        [HttpGet]
        public IActionResult DeleteProject(int id)
        {
            bool success;
            try
            {
                if (id != null)
                {
                    _manager.DeleteProject(id);
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HomeController.DeleteProject");
                return Json(new { success = false });
            }
            
        }


        public IActionResult Contact()
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
