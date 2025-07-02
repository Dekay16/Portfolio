using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Business.ViewModels
{
    public class ProjectsViewModel
    {
        public int? ID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Technologies { get; set; }
        [Required]
        [Url]
        public string? GitHubLink { get; set; }
        public string? Extra { get; set; }

        public IFormFile ImageFile { get; set; }
        public string? FilePath { get; set; }
    }
}
