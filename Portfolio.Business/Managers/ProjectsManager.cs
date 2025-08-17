using Portfolio.Business.Interfaces;
using Portfolio.Business.ViewModels;
using Portfolio.Context.Models;
using Portfolio.Data;

namespace Portfolio.Business.Managers
{
    public class ProjectsManager : IProjectsManager
    {
        private readonly ApplicationDbContext _context;
        private readonly IErrorLogger _logger;
        public ProjectsManager(ApplicationDbContext context, IErrorLogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public List<ProjectsViewModel> GetAllProjects()
        {
            try
            {
                var projects = _context.Projects.Select(p => new ProjectsViewModel
                {
                    ID = p.ID,
                    Title = p.Title,
                    Description = p.Description,
                    Technologies = p.Technologies,
                    GitHubLink = p.GitHubLink,
                    Extra = p.Extra,
                })
                    .ToList();

                return projects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProjectManager.GetAllProjects");
                throw;
            }
        }

        public ProjectsViewModel GetProjectById(int id)
        {
            try
            {
                var project = _context.Projects.Find(id);

                ProjectsViewModel vm = new ProjectsViewModel
                {
                    ID = project.ID,
                    Title = project.Title,
                    Description = project.Description,
                    Technologies= project.Technologies,
                    GitHubLink = project.GitHubLink,
                    Extra = project.Extra,
                };

                return vm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProjectManager.GetProjectById");
                throw;
            }
        }

        public void AddProject(ProjectsViewModel vm)
        {
            try
            {
                var project = new PortfolioProject();

                project.Title = vm.Title;
                project.Description = vm.Description;
                project.Technologies = vm.Technologies;
                project.GitHubLink = vm.GitHubLink;
                project.Extra = vm.Extra;

                _context.Projects.Add(project);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProjectManager.AddProject");
                throw;
            }
        }

        public void EditProject(ProjectsViewModel vm)
        {
            try
            {
                var project = _context.Projects.Find(vm.ID);

                project.ID = vm.ID.Value;
                project.Title = vm.Title;
                project.Description = vm.Description;
                project.Technologies = vm.Technologies;
                project.Extra= vm.Extra;
                project.GitHubLink = vm.GitHubLink;

                _context.Update(project);
                _context.SaveChanges();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error in ProjectManager.EditProject");
                throw;
            }
        }

        public void DeleteProject(int id)
        {
            try
            {
                var project = _context.Projects.Find(id);

                if (project.ID != null)
                {
                    _context.Remove(project);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProjectManager.DeleteProject");
                throw;
            }
        }

    }
}
