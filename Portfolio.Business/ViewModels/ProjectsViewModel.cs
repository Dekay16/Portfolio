using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Business.ViewModels
{
    public class ProjectsViewModel
    {
        public int? ID { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Technologies { get; set; }
        public string? GitHubLink { get; set; }
        public string? Extra { get; set; }
    }
}
