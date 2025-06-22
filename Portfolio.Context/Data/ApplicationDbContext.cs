using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context.Interfaces;
using Portfolio.Context.Models;

namespace Portfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public DbSet<PortfolioProject> Projects { get; set; }
        public DbSet<ErrorLog> Errors { get; set; }
        public DbSet<TrafficLog> TrafficLog { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
