using Microsoft.EntityFrameworkCore;
using Portfolio.Context.Models;

namespace Portfolio.Context.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ErrorLog> Errors { get; set; }
        DbSet<PortfolioProject> Projects { get; set; }
        DbSet<TrafficLog> TrafficLog { get; set; }
    }
}