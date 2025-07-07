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

            //if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            //{
            //    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            //    // Ensure the folder exists
            //    if (!Directory.Exists(uploadsFolder))
            //    {
            //        Directory.CreateDirectory(uploadsFolder);
            //    }

            //    // Generate unique file name if needed to avoid conflicts
            //    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
            //    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            //    // Save the file to wwwroot/images
            //    using (var fileStream = new FileStream(filePath, FileMode.Create))
            //    {
            //        vm.ImageFile.CopyTo(fileStream);
            //    }



                if (vm.ID == 0 || vm.ID == null)
                {
                    var model = new ProjectsViewModel
                    {
                        Title = vm.Title,
                        Description = vm.Description,
                        Technologies = vm.Technologies,
                        GitHubLink = vm.GitHubLink,
                        Extra = vm.Extra,
                        //FilePath = $"/images/{uniqueFileName}"
                    };

                    _manager.AddProject(model);

                }
                else
                {
                    _manager.EditProject(vm);
                }
            //}

            return Json(new { success = true });
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
                return Json(new { success = false });
            }
            
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
