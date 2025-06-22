using Portfolio.Business.ViewModels;

namespace Portfolio.Business.Interfaces
{
    public interface IProjectsManager
    {
        List<ProjectsViewModel> GetAllProjects();
        void AddProject(ProjectsViewModel vm);
        void DeleteProject(int id);
        void EditProject(ProjectsViewModel vm);
        ProjectsViewModel GetProjectById(int id);
    }
}