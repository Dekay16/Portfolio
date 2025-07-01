using Portfolio.Business.Interfaces;
using Portfolio.Business.ViewModels;
using Portfolio.Context.Models;
using Portfolio.Data;

namespace Portfolio.Business.Managers
{
    public class ProjectsManager : IProjectsManager
    {
        private readonly ApplicationDbContext _context;
        public ProjectsManager(ApplicationDbContext context)
        {
            _context = context;
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
                throw;
            }
        }

    }
}
